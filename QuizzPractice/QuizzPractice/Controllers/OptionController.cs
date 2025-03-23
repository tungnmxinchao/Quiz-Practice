using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizzPractice.DTOs.Request;
using QuizzPractice.Interface;

namespace QuizzPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {

        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOptionsByQuestionId([FromBody] List<UpdateOptionRequest> request)
        {
            try
            {
                var response = await _optionService.UpdateOptionByQuestionId(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating options. Details: {ex.Message}");
            }
        }
    }
}
