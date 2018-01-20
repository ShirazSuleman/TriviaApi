using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Game : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public bool Complete { get; set; }

        public ICollection<GameQuestion> GameQuestions { get; set; }
    }
}