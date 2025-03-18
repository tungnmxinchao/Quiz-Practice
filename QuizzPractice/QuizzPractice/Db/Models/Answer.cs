using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.Db.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int ResultId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerContent { get; set; }
        public bool? IsCorrect { get; set; }
        public float? Score { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now; 

 
        public Result Result { get; set; }
        public Question Question { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
    }
}
