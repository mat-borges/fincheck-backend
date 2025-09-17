using Fincheck.Application.DTOs.Auth;
using Fincheck.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fincheck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            if (result.Success)
                return Ok(new { message = result.Message });
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.ValidateUserAsync(dto);
            if (result.Success)
                return Ok(new { token = result.Token, refreshToken = result.RefreshToken });
            return Unauthorized(result.Message);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = _authService.RefreshToken(dto.RefreshToken);
            if (result.Success)
                return Ok(new { token = result.Token });
            return Unauthorized(result.Message);
        }
    }
}