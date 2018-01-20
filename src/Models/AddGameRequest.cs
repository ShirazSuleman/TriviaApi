using System.ComponentModel.DataAnnotations;

namespace TriviaApi
{
    public class AddGameRequest
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
    }
}