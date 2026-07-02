using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.BLL.DTOs.Auth;
using TodoApp.BLL.Interfaces;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await authService.RegisterAsync(registerRequestDto);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    message = "User was registered! Please login."
                });
            }
            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var jwtToken = await authService.LoginAsync(loginRequestDto);

            if (jwtToken != null)
            {
                var response = new AuthResponseDto
                {
                    JwtToken = jwtToken
                };
                
                return Ok(response);
            }
    
            return Unauthorized("Invalid email or password.");
        }
    }
}
