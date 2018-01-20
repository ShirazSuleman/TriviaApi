using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Game
    {        
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public bool IsComplete { get; set; }

        public int TotalScore { get; set; }

        public int TimeToAnswer { get; set; }

        public ICollection<GameQuestion> GameQuestions { get; set; }
    }
}