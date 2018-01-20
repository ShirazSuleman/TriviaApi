using System.Collections.Generic;

namespace TriviaApi
{
    public interface IGamesRepository
    {
        Game GetById(long gameId);
        void Add(Game game);
        Game GetGameInformation(long gameId);
        GameQuestion GetGameQuestion(long gameId, long gameQuestionId);
        void SubmitAnswer(long gameId, long gameQuestionId, long answerId, int secondsElapsed);
        bool ValidateAnswerToQuestion(long answerId, long gameQuestionId);
    }
}