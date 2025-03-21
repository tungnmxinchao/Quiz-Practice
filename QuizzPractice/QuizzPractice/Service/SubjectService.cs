using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db;
using QuizzPractice.Db.Models;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;
using QuizzPractice.Utils;

namespace QuizzPractice.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public SubjectService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSubjectResponse> CreateSubject(CreateSubjectRequest request)
        {
            var subject = _mapper.Map<Subject>(request);

            if (subject == null)
            {
                throw new Exception("Subject not found!");
            }

            //example teacher id = 1
            subject.CreatedBy = 1;

            await _context.AddAsync(subject);

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetSubjectResponse>(subject);

            return response;
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.
                FirstOrDefaultAsync(q => q.SubjectId == id);
            if (subject == null)
            {
                throw new Exception("Subject not found!");
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetSubjectResponse>> FindAll()
        {
            var subjects = await _context.Subjects
                .Include(c => c.CreatedByUser)
                .ToListAsync();

            return _mapper.Map<List<GetSubjectResponse>>(subjects);
        }

        public async Task<GetSubjectResponse> FindSubjectById(int id)
        {
            var subject = await _context.Subjects.
                FirstOrDefaultAsync(q => q.SubjectId == id);
            if (subject == null)
            {
                throw new Exception("Subject not found!");
            }
            return _mapper.Map<GetSubjectResponse>(subject);
        }

        public async Task<GetSubjectResponse> UpdateSubject(UpdateSubjectRequest request, int subjectId)
        {
            var subject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectId == subjectId);

            if (subject == null)
            {
                throw new Exception("Subject not found!");
            }

            _mapper.Map(request, subject);

            subject.CreatedBy = 1;

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetSubjectResponse>(subject);

            return response;
        }
    }
}
