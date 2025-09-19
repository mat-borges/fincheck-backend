using FinCheck.Application.DTOs.Auth;
using FinCheck.Domain.Models;

namespace FinCheck.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> ValidateUserAsync(LoginRequestDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}