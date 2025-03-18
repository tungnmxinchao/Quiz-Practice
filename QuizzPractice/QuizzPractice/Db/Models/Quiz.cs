namespace QuizzPractice.Db.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public string QuizCode { get; set; }
        public int TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }


        public User Teacher { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
