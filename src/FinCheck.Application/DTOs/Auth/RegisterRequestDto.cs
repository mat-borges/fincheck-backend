using System.ComponentModel.DataAnnotations;

namespace Fincheck.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
		[Required, EmailAddress]
    	public string Email { get; set; } = string.Empty;
		[Required, MinLength(8)]
    	public string Password { get; set; } = string.Empty;
		[Required]
    	public string DisplayName { get; set; } = string.Empty;
    	public string BaseCurrency { get; set; } = "BRL";
    	public DateTime? BirthDate { get; set; }
    }
}