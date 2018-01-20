using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class SubmitAnswerRequest
    {
        public long AnswerId { get; set; }

        [Range(0, int.MaxValue)]
        public int SecondsElapsed { get; set; }
    }
}