using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface IOptionService
    {
        public Task<List<GetOptionResponse>> UpdateOptionByQuestionId(List<UpdateOptionRequest> request);
    }
}
