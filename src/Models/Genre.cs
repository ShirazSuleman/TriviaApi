using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TriviaApi
{
    public class Genre
    {
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}

