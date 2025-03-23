using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using QuizzPractice.DTOs.Request;
using QuizzPractice.Interface;

namespace QuizzPractice.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [Authorize(Policy = "TeacherOrStudent")]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await _questionService.FindAll();
            return Ok(questions);
        }

        [Authorize(Policy = "TeacherOrStudent")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var question = await _questionService.FindQuestionById(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionRequest request)
        {

            try
            {
                var response = await _questionService.CreateQuestion(request);
                return CreatedAtAction(nameof(GetById), new { id = response.QuestionId }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionRequest request)
        {


            try
            {
                var response = await _questionService.UpdateQuestion(request, id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var result = await _questionService.DeleteQuestion(id);
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
