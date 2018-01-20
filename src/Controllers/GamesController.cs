using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TriviaApi
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameQuestionRepository _gameQuestionRepository;

        public GamesController(IGameRepository gameRepository, IGameQuestionRepository gameQuestionRepository)
        {
            _gameRepository = gameRepository;
            _gameQuestionRepository = gameQuestionRepository;
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {
            var game = _gameRepository.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return new ObjectResult(game);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            _gameRepository.Add(game);
            _gameQuestionRepository.AddGame(game);

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }
    }
}