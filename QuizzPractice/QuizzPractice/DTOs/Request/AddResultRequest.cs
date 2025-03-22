using QuizzPractice.Db.Models;

namespace QuizzPractice.DTOs.Request
{
    public class AddResultRequest
    {
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public ICollection<AnswerRequest> Answers { get; set; }
    }

    public class AnswerRequest
    {
        public int QuestionId { get; set; }
        public string AnswerContent { get; set; }
        public int CreatedBy { get; set; }
    }


}
