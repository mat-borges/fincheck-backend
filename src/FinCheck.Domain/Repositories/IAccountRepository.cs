using FinCheck.Domain.Models;

namespace FinCheck.Domain.Repositories
{
	public interface IAccountRepository
	{
		Task<Account?> GetByIdAsync(Guid id);
		Task<List<Account>> GetAllByUserAsync(Guid userId);
		Task AddAccountAsync(Account account);
		Task UpdateAsync(Account account);
	}
}