using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fincheck.Domain.Models
{
    public partial class Account:BaseEntity
    {
    	[Required]
    	public Guid UserId { get; set; }

    	[ForeignKey("UserId")]
    	public User User { get; set; } = null!;

    	[Required, MaxLength(100)]
    	public string Name { get; set; } = string.Empty;

    	[Required, MaxLength(50)]
    	public string Type { get; set; } = string.Empty;

    	[Column(TypeName = "decimal(18,2)")]
    	public decimal Balance { get; set; }

    	// Navigation
    	public ICollection<Transaction> Transactions { get; set; } = [];
    }
}