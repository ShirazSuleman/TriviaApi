using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TriviaApi
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly TriviaContext _context;

        public GamesController(TriviaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
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

            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }
    }
}