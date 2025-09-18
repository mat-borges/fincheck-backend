using Fincheck.Application.DTOs.Auth;
using Fincheck.Domain.Models;
using Fincheck.Infrastructure.Repositories;
using FinCheck.Application.Config;
using FinCheck.Application.Seed;
using FinCheck.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fincheck.Application.Services
{
    public class AuthService(AuthRepository authRepository, CategoryRepository categoryRepository, IOptions<JwtSettings> jwtSettings, IOptions<AppSettings> appSettings)
    {
        private readonly AuthRepository _authRepository = authRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly AppSettings _appSettings = appSettings.Value;

        public async Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto dto)
        {
            if (await _authRepository.EmailExistsAsync(dto.Email))
                return new AuthResponseDto { Success = false, Message = "Email already registered." };

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                DisplayName = dto.DisplayName,
                BaseCurrency = dto.BaseCurrency,
                BirthDate = dto.BirthDate
            };

            await _authRepository.AddUserAsync(user);

            var defaultCategories = CategorySeeder.GetDefaultCategories(user.Id);
            await _categoryRepository.AddRangeAsync(defaultCategories);

            return new AuthResponseDto { Success = true, Message = "User registered successfully." };
        }

        public async Task<AuthResponseDto> ValidateUserAsync(LoginRequestDto dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return new AuthResponseDto { Success = false, Message = "Invalid credentials." };

            var token = GenerateJwtToken(user);
            var (Token, Expires) = GenerateRefreshToken();

            user.RefreshToken = Token;
            user.RefreshTokenExpiry = Expires;
            await _authRepository.UpdateUserAsync(user);

            return new AuthResponseDto { Success = true, Token = token, RefreshToken = Token };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _authRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                return new AuthResponseDto { Success = false, Message = "Invalid or expired refresh token." };

            var newToken = GenerateJwtToken(user);
            var (Token, Expires) = GenerateRefreshToken();

            user.RefreshToken = Token;
            user.RefreshTokenExpiry = Expires;
            await _authRepository.UpdateUserAsync(user);

            return new AuthResponseDto { Success = true, Token = newToken, RefreshToken = Token };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Email),
                    new(ClaimTypes.GivenName, user.DisplayName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static (string Token, DateTime Expires) GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return (Convert.ToBase64String(randomBytes), DateTime.UtcNow.AddDays(7));
        }
    }
}