using System.ComponentModel.DataAnnotations;

namespace Fincheck.Domain.Models
{
    public partial class User:BaseEntity
    {
    	[Required, EmailAddress]
    	public string Email { get; set; } = string.Empty;

    	[Required, MaxLength(256)]
    	public byte[]? PasswordHash { get; set; }

    	[Required, MaxLength(120)]
    	public string DisplayName { get; set; } = "";

    	[Required, StringLength(3)]
    	public string BaseCurrency { get; set; } = "BRL";

    	public DateTime? BirthDate { get; set; }

    	public ICollection<Account> Accounts { get; set; } = [];
    	public ICollection<Category> Categories { get; set; } = [];
    	public ICollection<Transaction> Transactions { get; set; } = [];
    }
}