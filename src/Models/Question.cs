using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Question
    {
        public long Id { get; set; }

        [Required]
        public string Text { get; set; }

        public List<Answer> Answers { get; set; }

        public Genre Genre { get; set; }
    }
}