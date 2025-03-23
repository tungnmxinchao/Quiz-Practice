using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Helper;
using QuizzPractice.Interface;
using QuizzPractice.Utils;

namespace QuizzPractice.Service
{
    public class QuizService : IQuizService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuizService(QuizDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetQuizResponse>> FindAll()
        {
            var quizzes = await _context.Quizzes
                .Include(s => s.Subject)
                .Include(t => t.Teacher)
                .ToListAsync();

            return _mapper.Map<List<GetQuizResponse>>(quizzes);
        }

        public async Task<GetQuizResponse> CreateQuiz(CreateQuizRequest request)
        {
            var quiz = _mapper.Map<Quiz>(request);

            string code = GenerateData.GenerateRandomCode();

            int userId = JwtHelper.GetUserIdFromJwt(_httpContextAccessor.HttpContext);

            if (userId == -1)
            {
                throw new UnauthorizedAccessException("User ID not found in JWT.");
            }
            quiz.TeacherId = userId;

            quiz.QuizCode = code;



            if (quiz == null)
            {
                throw new Exception("Quiz not found!");
            }
            await _context.AddAsync(quiz);

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetQuizResponse>(quiz);

            return response;


        }

        public async Task<GetQuizResponse> UpdateQuiz(UpdateQuizRequest request, int quizId)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == quizId);

            if (quiz == null)
            {
                throw new Exception("Quiz not found!");
            }

            _mapper.Map(request, quiz);

            int userId = JwtHelper.GetUserIdFromJwt(_httpContextAccessor.HttpContext);

            if (userId == -1)
            {
                throw new UnauthorizedAccessException("User ID not found in JWT.");
            }

            quiz.TeacherId = userId;

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetQuizResponse>(quiz);

            return response;
        }

        public async Task<GetQuizResponse> FindQuizById(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == id);
            if (quiz == null)
            {
                throw new Exception("Quiz not found!");
            }
            return _mapper.Map<GetQuizResponse>(quiz);
        }

        public async Task<bool> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == id);
            if (quiz == null)
            {
                throw new Exception("Quiz not found!");
            }
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> JoinQuiz(string quizCode, int quizId)
        {
            var quiz = _context.Quizzes
                 .FirstOrDefault(c => c.QuizCode.Equals(quizCode) && c.QuizId == quizId);

            if (quiz == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task<QuizCodeResponse> GetQuizCode(int quizId)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(x => x.QuizId == quizId);

            QuizCodeResponse response = new QuizCodeResponse()
            {
                QuizId = quizId,
                Code = quiz.QuizCode
            };

            return response;
        }
    }
}
