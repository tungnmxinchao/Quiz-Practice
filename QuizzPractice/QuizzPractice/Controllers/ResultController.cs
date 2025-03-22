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
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] AddResultRequest request)
        {
            try
            {
                var result = await _resultService.AddResult(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Code: 1001 - An error occurred while adding the result. Details: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResultById(int id)
        {
            var result = await _resultService.GetResultById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAllResults()
        {
            var results = await _resultService.FindAllResults();
            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, [FromBody] UpdateResultRequest request)
        {
            try
            {
                var result = await _resultService.UpdateResult(request, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Code: 1005 - An error occurred while updating the result. Details: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            try
            {
                var result = await _resultService.DeleteResult(id);
                if (result == null)
                {
                    return NotFound($"Error Code: 1002 - Result with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Code: 1002 - An error occurred while deleting the result. Details: {ex.Message}");
            }
        }

    }
}
