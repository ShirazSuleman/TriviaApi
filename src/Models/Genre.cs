using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Genre : BaseEntity
    {        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}

