namespace Fincheck.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
    	public string Email { get; set; } = string.Empty;
    	public string Password { get; set; } = string.Empty;
    	public string DisplayName { get; set; } = string.Empty;
    	public string BaseCurrency { get; set; } = "BRL";
    	public DateTime? BirthDate { get; set; }
    }
}