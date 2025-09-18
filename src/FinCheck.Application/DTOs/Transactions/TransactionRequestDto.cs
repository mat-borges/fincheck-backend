namespace Fincheck.Application.DTOs.Transactions
{
	public class TransactionRequestDto
	{
		public Guid AccountId { get; set; }
		public Guid? CategoryId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; }
		public string Type { get; set; } = string.Empty; // Income, Expense, Transfer
	}
}