using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fincheck.Domain.Models
{
    public partial class Category:BaseEntity
    {
    	public Guid? UserId { get; set; } // nullable if global category

    	[ForeignKey("UserId")]
    	public User? User { get; set; }

    	[Required, MaxLength(100)]
    	public string Name { get; set; } = string.Empty;

    	[MaxLength(50)]
    	public string? Type { get; set; } // "Income", "Expense", "Transfer"

    	// Navigation
    	public ICollection<Transaction> Transactions { get; set; } = [];
    }
}