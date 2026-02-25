using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VisionPlatform.Application.DTOs.Auth;
using VisionPlatform.Application.Interfaces;

namespace VisionPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // 🔐 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        // 🔒 ENDPOINT PROTEGIDO
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Role = role
            });
        }
        //temporario

        [HttpGet("generate-hash")]
        public IActionResult GenerateHash(string password)
        {
            return Ok(BCrypt.Net.BCrypt.HashPassword(password));
        }

    }
}
