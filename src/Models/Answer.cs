using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TriviaApi
{
    public class Answer
    {        
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("Question"), SetOnlyJsonProperty]
        public long QuestionId { get; set; }

        [Required, JsonIgnore]
        public Question Question { get; set; }
    }
}