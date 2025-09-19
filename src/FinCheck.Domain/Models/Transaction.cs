using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinCheck.Domain.Models
{
    public partial class Transaction:BaseEntity
    {
    	[Required]
    	public Guid UserId { get; set; }

    	[ForeignKey("UserId")]
    	public User User { get; set; } = null!;

    	[Required]
    	public Guid AccountId { get; set; }

    	[ForeignKey("AccountId")]
    	public Account Account { get; set; } = null!;

    	public Guid? CategoryId { get; set; }

    	[ForeignKey("CategoryId")]
    	public Category? Category { get; set; }

    	[Required, MaxLength(100)]
    	public string Title { get; set; } = string.Empty;

    	public string? Description { get; set; }

    	[Column(TypeName = "decimal(18,2)")]
    	public decimal Amount { get; set; }

    	public DateTime TransactionDate { get; set; }

    	[Required]
    	public TransactionType Type { get; set; }
    }
}