using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface IQuestionService
    {
        public Task<List<GetQuestionResponse>> FindAll();

        public Task<GetQuestionResponse> CreateQuestion(CreateQuestionRequest request);

        public Task<GetQuestionResponse> UpdateQuestion(UpdateQuestionRequest request, int quizId);
        Task<GetQuestionResponse> FindQuestionById(int id);
        Task<bool> DeleteQuestion(int id);

    }
}
