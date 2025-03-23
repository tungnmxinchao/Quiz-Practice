using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;

namespace QuizzPractice.Service
{
    public class QuestionService : IQuestionService
    {

        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public QuestionService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetQuestionResponse> CreateQuestion(CreateQuestionRequest request)
        {
            var question = _mapper.Map<Question>(request);

            if (question == null)
            {
                throw new Exception("Question not found!");
            }

            question.CreatedBy = 1;

            if (question.Options != null)
            {
                foreach (var option in question.Options)
                {
                    option.CreatedBy = question.CreatedBy;
                    option.QuestionId = question.QuestionId;
                }
            }

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetQuestionResponse>(question);
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.QuestionId == id);

            if (question == null)
            {
                throw new Exception("Question not found!");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetQuestionResponse>> FindAll()
        {
            var questions = await _context.Questions
                .Include(o => o.Options)
                .Include(c => c.CreatedByUser)
                .Include(q => q.Quiz)
                .ToListAsync();
            return _mapper.Map<List<GetQuestionResponse>>(questions);
        }

        public async Task<GetQuestionResponse> FindQuestionById(int id)
        {
            var question = await _context.Questions
                .Include(o => o.Options)
                .Include(c => c.CreatedByUser)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (question == null)
            {
                throw new Exception("Question not found!");
            }

            return _mapper.Map<GetQuestionResponse>(question);
        }

        public async Task<GetQuestionResponse> UpdateQuestion(UpdateQuestionRequest request, int questionId)
        {
            var question = await _context.Questions
                .Include(q => q.Options)
                .Include(q => q.CreatedByUser)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);

            if (question == null)
            {
                throw new Exception("Question not found!");
            }

            _mapper.Map(request, question);       

            await _context.SaveChangesAsync();
            return _mapper.Map<GetQuestionResponse>(question);
        }

    }
}
