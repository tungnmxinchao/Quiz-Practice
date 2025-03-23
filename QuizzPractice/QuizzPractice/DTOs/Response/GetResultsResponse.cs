using QuizzPractice.Db.Models;

namespace QuizzPractice.DTOs.Response
{
    public class GetResultsResponse
    {
        public int ResultId { get; set; }
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public string QuizCode { get; set; }
        public float Score { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public UserResponse Student { get; set; }
        public QuizResponse Quiz { get; set; }
        public ICollection<AnswerResponse> Answers { get; set; }
    }

    public class QuizResponse
    {
        public string Title { get; set; }
        public int TimeLimit { get; set; }
    }

    public class AnswerResponse
    {
        public string AnswerContent { get; set; }
        public bool? IsCorrect { get; set; }
    }



}
