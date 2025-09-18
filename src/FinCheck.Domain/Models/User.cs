using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Fincheck.Domain.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public partial class User : BaseEntity
	{
		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string PasswordHash { get; set; } = string.Empty;

		[Required, MaxLength(120)]
		public string DisplayName { get; set; } = "";

		[Required, StringLength(3)]
		public string BaseCurrency { get; set; } = "BRL";

		public DateTime? BirthDate { get; set; }

		public ICollection<Account> Accounts { get; set; } = [];
		public ICollection<Category> Categories { get; set; } = [];
		public ICollection<Transaction> Transactions { get; set; } = [];

		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiry { get; set; }
    }
}