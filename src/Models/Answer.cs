using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Answer
    {
        public long Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}