using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace TriviaApi
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly IConfiguration _configuration;

        public GamesController(IGamesRepository gamesRepository, IConfiguration configuration)
        {
            _gamesRepository = gamesRepository;
            _configuration = configuration;
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGame([FromRoute] long gameId)
        {
            var game = _gamesRepository.GetById(gameId);
            if (game == null)
            {
                return _createResponse(HttpStatusCode.NotFound, "Invalid game id.");
            }

            return _createResponse(HttpStatusCode.OK, data: _parseGameStatus(_gamesRepository.GetGameInformation(game.Id)));    
        }

        [HttpPost]
        public IActionResult AddGame([FromBody] AddGameRequest request)
        {
            if (request == null || request.Title == null)
            {
                return _createResponse(HttpStatusCode.BadRequest, "Invalid request.");
            }

            var game = new Game
            {
                Title = request.Title,
                TimeToAnswer = Convert.ToInt32(_configuration.GetSection("AppSettings")["TimeToAnswerInSeconds"])
            };

            _gamesRepository.Add(game);

            return _createResponse(HttpStatusCode.OK, data: _parseGameStatus(_gamesRepository.GetGameInformation(game.Id)));
        }

        [HttpPost("{gameId}/submitAnswer")]
        public IActionResult SubmitAnswer([FromRoute] long gameId, [FromBody] SubmitAnswerRequest request)
        {
            var game = _gamesRepository.GetById(gameId);
            if (game == null)
            {
                return _createResponse(HttpStatusCode.NotFound, "Invalid game id.");
            }

            var gameQuestion = _gamesRepository.GetGameQuestion(gameId, request.GameQuestionId);
            if (gameQuestion == null)
            {
                return _createResponse(HttpStatusCode.NotFound, "Invalid game question id.");
            }

            if (gameQuestion.ChosenAnswer != null)
            {
                return _createResponse(HttpStatusCode.BadRequest, "Question already answered.");
            }

            if (!_gamesRepository.ValidateQuestionAndAnswer(request.AnswerId, request.GameQuestionId))
            {
                return _createResponse(HttpStatusCode.BadRequest, "Invalid answer id for the question.");
            }

            _gamesRepository.AnswerQuestion(gameId, request.GameQuestionId, request.AnswerId, request.SecondsElapsed);

            return _createResponse(HttpStatusCode.OK);
        }

        private IActionResult _createResponse(HttpStatusCode code, string message = null, object data = null)
        {
            var response = new GenericResponse<object>();

            response.Meta.Code = (int)code;
            response.Meta.Status = code.ToString();
            
            response.Meta.Message = message;
            response.Data = data;

            return StatusCode(response.Meta.Code, response);
        }

        private object _parseGameStatus(Game game)
        {
            return new 
            {
                game.Id,
                game.Title,
                game.IsComplete,
                game.TimeToAnswer,
                game.TotalScore,
                GameQuestions = game.GameQuestions.Select(gq => new 
                {
                    gq.Id,
                    gq.Score,
                    Genre = gq.Genre.Name,
                    Question = gq.Question.Text,
                    gq.Question.Answers,
                    gq.ChosenAnswer,
                    gq.SecondsElapsedForAnswer
                })
            };
        }
    }
}