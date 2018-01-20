namespace TriviaApi
{
    public class SubmitAnswerRequest
    {
        public long GameQuestionId { get; set; }

        public long AnswerId { get; set; }

        public int SecondsElapsed { get; set; }
    }
}