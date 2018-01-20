using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace TriviaApi
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository _gamesRepository;

        private const int timeToAnswer = 60;

        public GamesController(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {
            var game = _gamesRepository.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            return new ObjectResult(_parseGameStatus(_gamesRepository.GetGameInformation(id)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            game.TimeToAnswer = timeToAnswer;
            _gamesRepository.Add(game);
            _gamesRepository.AddGameQuestions(game);

            return new ObjectResult(_parseGameStatus(_gamesRepository.GetGameInformation(game.Id)));
        }

        [HttpPost("{id}/gameQuestion/{gameQuestionId}/answer/{answerId}")]
        public IActionResult AnswerQuestion(long id, long gameQuestionId, long answerId, int secondsElapsed)
        {
            var game = _gamesRepository.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            var gameQuestion = _gamesRepository.GetQuestion(gameQuestionId);
            if (gameQuestion == null)
            {
                return NotFound();
            }

            if (gameQuestion.ChosenAnswer != null)
            {
                return BadRequest("Question already answered.");
            }

            if (!_gamesRepository.ValidateQuestionAndAnswer(answerId, gameQuestionId))
            {
                return BadRequest("Invalid answer for the question.");
            }

            _gamesRepository.AnswerQuestion(id, gameQuestionId, answerId, secondsElapsed);

            return Ok();
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