using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.Db.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
