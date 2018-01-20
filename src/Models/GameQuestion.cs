using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriviaApi
{
    public class GameQuestion : BaseEntity
    {
        public int? Score { get; set; }

        public bool? IsCorrect { get; set; }

        [ForeignKey("Game")]
        public long GameId { get; set; }

        [Required]
        public Game Game { get; set; }

        [ForeignKey("Genre")]
        public long GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [ForeignKey("Question")]
        public long QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        [ForeignKey("Answer")]
        public long? AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}