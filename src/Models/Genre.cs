using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class Genre 
    {
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}

