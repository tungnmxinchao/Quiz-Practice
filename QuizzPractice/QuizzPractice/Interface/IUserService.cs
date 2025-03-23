using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.Interface
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<GetUserResponse> GetUserByIdAsync(int userId);
        Task<IEnumerable<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> UpdateUserAsync(int userId, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int userId);
    }
}
