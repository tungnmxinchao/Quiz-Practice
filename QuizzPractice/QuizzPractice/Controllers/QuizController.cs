using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using QuizzPractice.DTOs.Request;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;

namespace QuizzPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var quizzes =  await _quizService.FindAll();

            return Ok(quizzes);
        }

        [Authorize(Policy = "Teacher")]
        [HttpGet("quiz-code/{quizId}")]
        public async Task<IActionResult> GetQuizCode(int quizId)
        {
            var quizCodeResponse = await _quizService.GetQuizCode(quizId);

            if (quizCodeResponse != null)
            {
                return Ok(quizCodeResponse);

            }
            return NotFound("Not Found Quiz Code!");


        }


        [Authorize(Policy = "Student")]
        [HttpPost("join-practice")]
        public async Task<IActionResult> PracticeQuiz([FromBody] JoinPracticeQuizRequest request)
        {
            var quizz = await _quizService.JoinQuiz(request.QuizCode , request.QuizId);

            if (quizz)
            {
                return Ok(quizz);
                
            }
            return Unauthorized("Wrong quiz code!");


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quiz = await _quizService.FindQuizById(id);
                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddQuiz([FromBody] CreateQuizRequest request)
        {
            try
            {
                var response = await _quizService.CreateQuiz(request);
                return CreatedAtAction(nameof(Get), new { id = response.QuizId }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateQuiz([FromBody] UpdateQuizRequest request, int quizId)
        {
            try
            {
                var response = await _quizService.UpdateQuiz(request, quizId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                var result = await _quizService.DeleteQuiz(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
