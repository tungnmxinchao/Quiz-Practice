using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;

namespace QuizzPractice.Service
{
    public class ResultService : IResultService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public ResultService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetResultsResponse> AddResult(AddResultRequest request)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.QuizId == request.QuizId);

            if (quiz == null)
            {
                throw new Exception("Quiz not found!");
            }

            var result = _mapper.Map<Result>(request);
            result.EndTime = DateTime.Now;
            result.StartTime = result.EndTime.Value.AddMinutes(-quiz.TimeLimit);
            result.QuizCode = quiz.QuizCode;
            result.Answers = new List<Answer>();

            float score = 0;
            int correctAnswers = 0;

            foreach (var answerRequest in request.Answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.QuestionId == answerRequest.QuestionId);
                if (question == null)
                {
                    throw new Exception("Question not found!");
                }

                bool isCorrect = question.Options.Any(o => o.Content == answerRequest.AnswerContent && o.IsCorrect);
                if (isCorrect)
                {
                    correctAnswers++;
                }

                var answer = new Answer
                {
                    QuestionId = answerRequest.QuestionId,
                    AnswerContent = answerRequest.AnswerContent,
                    IsCorrect = isCorrect,
                    CreatedBy = answerRequest.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                result.Answers.Add(answer);
            }

            score = (correctAnswers / (float)quiz.Questions.Count) * 10;
            result.Score = score;

            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetResultsResponse>(result);

            return response;
        }

        public async Task<GetResultsResponse> DeleteResult(int id)
        {
            var result = await _context.Results
                    .Include(r => r.Answers)
                    .FirstOrDefaultAsync(r => r.ResultId == id);

            if (result == null)
            {
                throw new Exception("Result not found!");
            }

            _context.Answers.RemoveRange(result.Answers);

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetResultsResponse>(result);
            return response;
        }

        public async Task<List<GetResultsResponse>> FindAllResults()
        {
            var results = await _context.Results
                .Include(s => s.Student)
                .Include(q => q.Quiz)
                .Include(r => r.Answers)
                .ToListAsync();

            var response = _mapper.Map<List<GetResultsResponse>>(results);
            return response;
        }

        public async Task<GetResultsResponse> GetResultById(int id)
        {
            var result = await _context.Results
                .Include(s => s.Student)
                .Include(q => q.Quiz)
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.ResultId == id);

            if (result == null)
            {
                throw new Exception("Result not found!");
            }

            var response = _mapper.Map<GetResultsResponse>(result);
            return response;
        }

        public async Task<GetResultsResponse> UpdateResult(UpdateResultRequest request, int resultId)
        {
            var result = await _context.Results
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.ResultId == resultId);

            if (result == null)
            {
                throw new Exception("Result not found!");
            }

            _mapper.Map(request, result);
            result.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetResultsResponse>(result);
            return response;
        }

    }
}
