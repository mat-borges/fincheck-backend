using FinCheck.Domain.Models;

namespace FinCheck.Domain.Repositories
{
	public interface ITransactionRepository
	{
		Task<Transaction> GetByIdAsync(Guid id, Guid userId);
		Task<IEnumerable<Transaction>> GetAllByUserAsync(Guid userId);
		Task AddAsync(Transaction transaction);
		Task UpdateAsync(Transaction transaction);
		Task DeleteAsync(Transaction transaction);
	}
}