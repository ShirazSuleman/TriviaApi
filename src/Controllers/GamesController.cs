using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TriviaApi
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }
    }
}