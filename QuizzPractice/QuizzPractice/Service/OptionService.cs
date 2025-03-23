using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;

namespace QuizzPractice.Service
{
    public class OptionService : IOptionService
    {

        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public OptionService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Policy = "Teacher")]
        public async Task<List<GetOptionResponse>> UpdateOptionByQuestionId(List<UpdateOptionRequest> request)
        {
            if (request == null || !request.Any())
            {
                throw new ArgumentException("Request cannot be null or empty");
            }

            var questionId = request[0].QuestionId;

            var existingOptions = await _context.Options
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();

            var requestOptionIds = request.Select(r => r.OptionId).ToList();

            foreach (var updateOption in request)
            {
                var option = existingOptions.FirstOrDefault(o => o.OptionId == updateOption.OptionId);

                if (option != null)
                {
                    option.Content = updateOption.Content;
                    option.IsCorrect = updateOption.IsCorrect;
                    option.Status = updateOption.Status;
                    option.UpdatedAt = DateTime.Now;
                }
                else
                {
                    var newOption = _mapper.Map<Option>(updateOption);
                    newOption.CreatedAt = DateTime.Now;
                    newOption.UpdatedAt = DateTime.Now;
                    _context.Options.Add(newOption);
                }
            }
            var optionsToDelete = existingOptions
                .Where(o => !requestOptionIds.Contains(o.OptionId))
                .ToList();

            if (optionsToDelete.Any())
            {
                _context.Options.RemoveRange(optionsToDelete);
            }

            await _context.SaveChangesAsync();

            var updatedOptions = await _context.Options
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();

            return _mapper.Map<List<GetOptionResponse>>(updatedOptions);
        }
    }
}
