using Fincheck.Domain.Models;

namespace Fincheck.Application.DTOs.Transactions
{
	public class TransactionResponseDto
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public Guid? CategoryId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; }
		public TransactionType Type { get; set; }
	}
}
