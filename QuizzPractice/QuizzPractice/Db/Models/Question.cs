using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.Db.Models
{
    public class Question
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


        public Quiz Quiz { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
