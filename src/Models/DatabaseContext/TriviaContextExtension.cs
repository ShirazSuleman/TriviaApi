using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace TriviaApi
{
    public static class TriviaContextExtension
    {
        public static bool AllMigrationsApplied(this TriviaContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this TriviaContext context)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new SetOnlyContractResolver() };

            // Ensure we have genres
            if (!context.Genres.Any())
            {
                var genres = JsonConvert.DeserializeObject<List<Genre>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "genres.json"), settings);
                context.AddRange(genres);
                context.SaveChanges();
            }

            // Ensure we have questions
            if (!context.Questions.Any())
            {
                var questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "questions.json"), settings);
                context.AddRange(questions);
                context.SaveChanges();
            }

            // Ensure we have answers
            if (!context.Answers.Any())
            {
                var answers = JsonConvert.DeserializeObject<List<Answer>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "answers.json"), settings);
                context.AddRange(answers);
                context.SaveChanges();
            }
        }
    }
}