namespace QuizzPractice.DTOs.Request
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
