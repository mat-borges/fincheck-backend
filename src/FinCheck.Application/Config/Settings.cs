using System.ComponentModel.DataAnnotations;

namespace FinCheck.Application.Config
{
    public class JwtSettings
    {
        [Required] public string Key { get; set; } = string.Empty;
        [Required] public string Issuer { get; set; } = string.Empty;
        [Required] public string Audience { get; set; } = string.Empty;
    }

    public class AppSettings
    {
        [Required] public string PasswordKey { get; set; } = string.Empty;
        [Required] public string TokenKey { get; set; } = string.Empty;
    }
}