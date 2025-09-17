using Fincheck.Application.DTOs.Auth;
using Fincheck.Domain.Models;
using Fincheck.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fincheck.Application.Services
{
    public class AuthService(IConfiguration config, AuthRepository authRepository)
    {
    	private readonly IConfiguration _config = config;
    	private readonly AuthRepository _authRepository = authRepository;

        public async Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto dto)
        {
            if (await _authRepository.EmailExistsAsync(dto.Email))
                return new AuthResponseDto { Success = false, Message = "Email already registered." };

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                DisplayName = dto.DisplayName,
                BaseCurrency = dto.BaseCurrency,
                BirthDate = dto.BirthDate
            };

            await _authRepository.AddUserAsync(user);

            return new AuthResponseDto { Success = true, Message = "User registered successfully." };
        }

        public async Task<AuthResponseDto> ValidateUserAsync(LoginRequestDto dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return new AuthResponseDto { Success = false, Message = "Invalid credentials." };

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Store refreshToken with user if you want persistent refresh tokens

            return new AuthResponseDto { Success = true, Token = token, RefreshToken = refreshToken };
        }

        public AuthResponseDto RefreshToken(string refreshToken)
        {
            // Validate refresh token logic here (e.g., check against DB or in-memory store)
            // For demo, just return a new JWT
            var token = GenerateJwtToken(null); // You'd need to get the user from the refresh token
            return new AuthResponseDto { Success = true, Token = token };
        }

        private static byte[] HashPassword(string password)
    	{
    		return SHA256.HashData(Encoding.UTF8.GetBytes(password));
    	}

    	private static bool VerifyPassword(string password, byte[]? hash)
        {
            if (hash == null) return false;
            var computed = HashPassword(password);
            return hash.SequenceEqual(computed);
        }

        private string GenerateJwtToken(User? user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user?.Email ?? "unknown"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}