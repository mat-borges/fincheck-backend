using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinCheck.Domain.Models
{
    public partial class Category:BaseEntity
    {
    	public Guid UserId { get; set; }

    	[ForeignKey("UserId")]
    	public User? User { get; set; }

    	[Required, MaxLength(100)]
    	public string Name { get; set; } = string.Empty;

    	[Required]
    	public CategoryType Type { get; set; }

    	// Navigation
    	public ICollection<Transaction> Transactions { get; set; } = [];
    }
}