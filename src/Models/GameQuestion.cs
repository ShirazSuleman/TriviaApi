using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TriviaApi
{
    public class GameQuestion
    {
        public long Id { get; set; }

        public int? Score { get; set; }

        public bool? IsCorrect { get; set; }

        public int? SecondsElapsedForAnswer { get; set; }

        [ForeignKey("Game"), JsonIgnore]
        public long GameId { get; set; }

        [Required, JsonIgnore]
        public Game Game { get; set; }

        [ForeignKey("Genre"), JsonIgnore]
        public long GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [ForeignKey("Question"), JsonIgnore]
        public long QuestionId { get; set; }

        [Required, JsonIgnore]
        public Question Question { get; set; }

        [ForeignKey("ChosenAnswer"), JsonIgnore]
        public long? AnswerId { get; set; }

        public Answer ChosenAnswer { get; set; }
    }
}