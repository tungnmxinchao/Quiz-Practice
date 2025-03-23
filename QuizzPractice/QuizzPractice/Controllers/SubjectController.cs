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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getSubjects = await _subjectService.FindAll();

            return Ok(getSubjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var subject = await _subjectService.FindSubjectById(id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] CreateSubjectRequest request)
        {
            try
            {
                var response = await _subjectService.CreateSubject(request);
                return CreatedAtAction(nameof(Get), new { id = response.SubjectId }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody] UpdateSubjectRequest request, int subjectId)
        {
            try
            {
                var response = await _subjectService.UpdateSubject(request, subjectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var result = await _subjectService.DeleteSubject(id);
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
