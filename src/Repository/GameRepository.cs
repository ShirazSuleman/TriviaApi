using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TriviaApi
{
    public class GameRepository : IGameRepository
    {
        private readonly TriviaContext _context;

        public GameRepository(TriviaContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> ListAll() => _context.Games.AsEnumerable();

        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public Game GetById(long id)
        {
            return _context.Games.Include(g => g.GameQuestions).FirstOrDefault(g => g.Id == id);
        }
    }
}