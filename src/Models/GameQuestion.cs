namespace TriviaApi
{
    public class GameQuestion
    {
        public long Id { get; set; }

        public int Score { get; set; }

        public Game Game { get; set; }

        public Genre Genre { get; set; }

        public Question Question { get; set; }

        public Answer Answer { get; set; }
    }
}