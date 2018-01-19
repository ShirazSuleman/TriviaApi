
using Microsoft.EntityFrameworkCore;

namespace TriviaApi
{
    public class TriviaContext : DbContext
    {
        public TriviaContext(DbContextOptions<TriviaContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<GameQuestion> GameQuestions { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>()
                    .HasIndex(g => g.Title)
                    .IsUnique();

            builder.Entity<Genre>()
                    .HasIndex(g => g.Name)
                    .IsUnique();

            builder.Entity<Question>()
                    .HasIndex(g => g.Text)
                    .IsUnique();
        }

    }
}