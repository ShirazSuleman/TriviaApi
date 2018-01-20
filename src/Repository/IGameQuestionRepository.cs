using System.Collections.Generic;

namespace TriviaApi
{
    public interface IGameQuestionRepository
    {
        IEnumerable<GameQuestion> GetByGameId(long id);
        void AddGame(Game game);
    }
}