using QuizzPractice.Db.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.DTOs.Response
{
    public class GetQuestionResponse
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string Level { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string Status { get; set; }


        public GetQuizResponse Quiz { get; set; }

        public UserResponse CreatedByUser { get; set; }
        public ICollection<OptionResponse> Options { get; set; }
    }

    public class OptionResponse
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string Status { get; set; }
    }
}
