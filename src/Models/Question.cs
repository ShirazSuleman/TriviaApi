using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriviaApi
{
    public class Question : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public ICollection<Answer> Answers { get; set; }

        [ForeignKey("Genre")]
        public long GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}