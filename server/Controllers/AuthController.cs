using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] UserDto newUserDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                var message = await userService.RegisterUser(newUserDto);
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Details = ex.Message });
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email or password is empty");
            }

            try
            {
                var tkn = await userService.LoginUser(request.Email, request.Password);
                return Ok(new {token = tkn});

            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = "Login failed", Details = e.Message });
            }
        }

        [HttpGet("users")]
        
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            return await userService.GetUserByEmail(email);
        }
    }
}
