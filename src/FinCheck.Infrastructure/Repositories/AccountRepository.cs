using FinCheck.Domain.Models;
using FinCheck.Infrastructure.Data;
using FinCheck.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinCheck.Infrastructure.Repositories
{
	public class AccountRepository(DataContextEF context) : IAccountRepository
	{
		private readonly DataContextEF _context = context;

		public async Task<Account?> GetByIdAsync(Guid id) => await _context.Accounts.FindAsync(id);
		public async Task<List<Account>> GetAllByUserAsync(Guid userId) => await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();

		public async Task AddAccountAsync(Account account)
		{
			await _context.Accounts.AddAsync(account);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Account account)
		{
			_context.Accounts.Update(account);
			await _context.SaveChangesAsync();
		}

	}
}