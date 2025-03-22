using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface IResultService
    {
        public Task<GetResultsResponse> AddResult(AddResultRequest request);

        public Task<GetResultsResponse> UpdateResult(UpdateResultRequest request, int resultId);

        public Task<GetResultsResponse> DeleteResult(int id);

        public Task<GetResultsResponse> GetResultById(int id);

        public Task<List<GetResultsResponse>> FindAllResults();
    }
}
