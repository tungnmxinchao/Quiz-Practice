using System.ComponentModel.DataAnnotations.Schema;
using QuizzPractice.Db.Models;

namespace QuizzPractice.DTOs.Response
{
    public class GetQuizResponse
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }

        public UserResponse Teacher { get; set; }
        public SubjectResponse Subject { get; set; }
    }

    public class QuestionResponse
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string Level { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }

    public class UserResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class SubjectResponse
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }
}
