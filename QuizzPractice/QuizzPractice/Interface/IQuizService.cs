using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface IQuizService
    {
        public Task<List<GetQuizResponse>> FindAll();

        public Task<GetQuizResponse> CreateQuiz(CreateQuizRequest request);

        public Task<GetQuizResponse> UpdateQuiz(UpdateQuizRequest request, int quizId);
        Task<GetQuizResponse> FindQuizById(int id);
        Task<bool> DeleteQuiz(int id);

        Task<bool> JoinQuiz(string quizCode, int quizId);

        Task<QuizCodeResponse> GetQuizCode(int quizId);
    }
}
