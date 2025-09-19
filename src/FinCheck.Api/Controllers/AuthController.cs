using FinCheck.Application.DTOs.Auth;
using FinCheck.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterUserAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.ValidateUserAsync(dto);
            return result.Success ? Ok(result) : Unauthorized(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
            return result.Success ? Ok(result) : Unauthorized(result);
        }
    }
}