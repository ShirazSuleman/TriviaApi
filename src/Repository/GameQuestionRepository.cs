using System.Collections.Generic;
using System.Linq;

namespace TriviaApi
{
    public class GameQuestionRepository : IGameQuestionRepository
    {
        private readonly TriviaContext _context;

        public GameQuestionRepository(TriviaContext context)
        {
            _context = context;
        }

        public void AddGame(Game game)
        {
            _context.GameQuestions.AddRange(_context.Genres.Join(_context.Questions, g => g.Id, q => q.GenreId, (g, q) => new GameQuestion { Game = game, Genre = g, Question = q }));
            _context.SaveChanges();
        }

        public IEnumerable<GameQuestion> GetByGameId(long id)
        {
            return _context.GameQuestions.Where(gq => gq.GameId == id);
        }
    }
}