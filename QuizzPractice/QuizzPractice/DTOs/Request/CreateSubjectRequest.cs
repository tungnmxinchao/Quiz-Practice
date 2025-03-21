using QuizzPractice.Db.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzPractice.DTOs.Request
{
    public class CreateSubjectRequest
    {
        public string SubjectName { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
    }
}
