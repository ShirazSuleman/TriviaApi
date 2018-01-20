using System.Collections.Generic;

namespace TriviaApi
{
    public interface IGamesRepository
    {
        Game GetById(long gameId);
        void Add(Game game);
        Game GetGameInformation(long gameId);
        GameQuestion GetGameQuestion(long gameId, long gameQuestionId);
        void AnswerQuestion(long gameId, long gameQuestionId, long answerId, int secondsElapsed);
        bool ValidateQuestionAndAnswer(long answerId, long gameQuestionId);
    }
}