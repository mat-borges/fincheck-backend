namespace Fincheck.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}