using System.ComponentModel.DataAnnotations;
using QuizzPractice.DTOs.Response;

namespace QuizzPractice.DTOs.Request
{
    public class CreateQuizRequest
    {
        [Required]
        public int QuizId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        [Range(1, 180)]
        public int TimeLimit { get; set; }

        [Required]
        [RegularExpression("active|inactive", ErrorMessage = "Status must be either 'active' or 'inactive'.")]
        public string Status { get; set; }
    }
}
