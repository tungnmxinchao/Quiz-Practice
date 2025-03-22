using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizzPractice.DTOs.Request;
using QuizzPractice.Interface;

namespace QuizzPractice.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            try
            {
                var response = await _userService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized($"Error Code: 2001 - An error occurred while logging in. Details: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Code: 2002 - An error occurred while registering the user. Details: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserByIdAsync(id);
                if (response == null)
                {
                    return NotFound($"Error Code: 2003 - User with ID {id} not found.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Code: 2003 - An error occurred while retrieving the user. Details: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _userService.GetAllUsersAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Code: 2004 - An error occurred while retrieving the users. Details: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Code: 2005 - An error occurred while updating the user. Details: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound($"Error Code: 2006 - User with ID {id} not found.");
                }

                return Ok($"User with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Code: 2006 - An error occurred while deleting the user. Details: {ex.Message}");
            }
        }


    }
}
