using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriviaApi
{
    public class Answer : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]
        public long QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }
    }
}