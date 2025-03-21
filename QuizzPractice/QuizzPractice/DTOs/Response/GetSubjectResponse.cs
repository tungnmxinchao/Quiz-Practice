using QuizzPractice.Db.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.DTOs.Response
{
    public class GetSubjectResponse
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string Status { get; set; }
        public  UserResponse CreatedByUser { get; set; }
    }

}
