namespace QuizzPractice.DTOs.Request
{
    public class UpdateResultRequest
    {
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public string QuizCode { get; set; }
        public float Score { get; set; }
    }
}
