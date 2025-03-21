using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface ISubjectService
    {
        public Task<List<GetSubjectResponse>> FindAll();

        public Task<GetSubjectResponse> CreateSubject(CreateSubjectRequest request);

        public Task<GetSubjectResponse> UpdateSubject(UpdateSubjectRequest request, int quizId);
        Task<GetSubjectResponse> FindSubjectById(int id);
        Task<bool> DeleteSubject(int id);
    }
}
