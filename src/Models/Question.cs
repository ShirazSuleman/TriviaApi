using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TriviaApi
{
    public class Question
    {
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public ICollection<Answer> Answers { get; set; }

        [ForeignKey("Genre"), SetOnlyJsonProperty]
        public long GenreId { get; set; }

        [Required, JsonIgnore]
        public Genre Genre { get; set; }
    }
}