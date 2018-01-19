using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Game
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Complete { get; set; }

        public List<GameQuestion> GameQuestions { get; set; }
    }
}