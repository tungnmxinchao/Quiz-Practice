using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.Db.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }

       
        public Question Question { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
    }
}
