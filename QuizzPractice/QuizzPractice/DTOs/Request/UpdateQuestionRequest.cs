using QuizzPractice.Db.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.DTOs.Request
{
    public class UpdateQuestionRequest 
    {
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
    }

    public class UpdateOptionRequest
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public int CreatedBy { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public string Status { get; set; }

    }
}
