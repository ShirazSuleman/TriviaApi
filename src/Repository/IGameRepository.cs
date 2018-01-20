using System.Collections.Generic;

namespace TriviaApi
{
    public interface IGameRepository
    {
        Game GetById(long id);
        IEnumerable<Game> ListAll();
        void Add(Game character);
    }
}