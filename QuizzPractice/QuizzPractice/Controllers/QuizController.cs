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
