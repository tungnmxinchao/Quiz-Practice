using QuizzPractice.Db.Models;

namespace QuizzPractice.DTOs.Request
{
    public class CreateQuestionRequest
    {
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public ICollection<OptionRequest> Options { get; set; }
    }

    public class OptionRequest
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public string Status { get; set; }

    }
}
