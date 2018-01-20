using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TriviaApi
{
    public class GamesRepository : IGamesRepository
    {
        private readonly TriviaContext _context;

        public GamesRepository(TriviaContext context)
        {
            _context = context;
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
            _addGameQuestions(game);
            _context.SaveChanges();
        }

        public Game GetById(long gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }

        public Game GetGameInformation(long gameId)
        {
            return _getGameInformation(gameId);
        }

        public bool ValidateQuestionAndAnswer(long answerId, long gameQuestionId)
        {
            var gameQuestion = _context.GameQuestions.First(gq => gq.Id == gameQuestionId);
            var chosenAnswer = _context.Answers.FirstOrDefault(a => a.Id == answerId && a.QuestionId == gameQuestion.QuestionId);

            return chosenAnswer != null;
        }

        public void AnswerQuestion(long gameId, long gameQuestionId, long answerId, int secondsElapsed)
        {
            var game = _getGameInformation(gameId);

            var score = game.TimeToAnswer - secondsElapsed;

            if (score < 0)
            {
                score = 0;
            }

            var gameQuestion = _context.GameQuestions.First(gq => gq.Id == gameQuestionId);
            var chosenAnswer = _context.Answers.FirstOrDefault(a => a.Id == answerId && a.QuestionId == gameQuestion.QuestionId);

            gameQuestion.ChosenAnswer = chosenAnswer;
            gameQuestion.IsCorrect = chosenAnswer.IsCorrect;

            score = chosenAnswer.IsCorrect ? score : 0;

            gameQuestion.Score = score;
            gameQuestion.SecondsElapsedForAnswer = secondsElapsed;

            _updateGameScore(gameId, score);

            _checkGameCompletionStatus(gameId);

            _context.SaveChanges();
        }

        public GameQuestion GetGameQuestion(long gameId, long gameQuestionId)
        {
            return _context.GameQuestions.Include(gq => gq.ChosenAnswer).FirstOrDefault(gq => gq.GameId == gameId && gq.Id == gameQuestionId);
        }

        private void _updateGameScore(long gameId, int points)
        {
            var game = _getGameInformation(gameId);
            game.TotalScore += points;
        }

        private void _checkGameCompletionStatus(long gameId)
        {
            var game = _getGameInformation(gameId);
            game.IsComplete = game.GameQuestions.All(gq => gq.ChosenAnswer != null);
        }

        private void _addGameQuestions(Game game)
        {
            _context.GameQuestions.AddRange(_context.Genres.Join(_context.Questions, g => g.Id, q => q.GenreId, (g, q) => new GameQuestion { Game = game, Genre = g, Question = q }));
        }

        private Game _getGameInformation(long gameId)
        {
            return _context.Games.Include(g => g.GameQuestions)
                                 .ThenInclude(gq => gq.Genre)
                                 .ThenInclude(g => g.Questions)
                                 .ThenInclude(q => q.Answers)
                                 .FirstOrDefault(g => g.Id == gameId);
        }
    }
}