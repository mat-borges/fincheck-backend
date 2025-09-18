using Fincheck.Domain.Models;
using Fincheck.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fincheck.Infrastructure.Repositories
{
	public class TransactionRepository(DataContextEF context)
	{
		private readonly DataContextEF _context = context;

		public async Task<Transaction> GetByIdAsync(Guid id, Guid userId)
		{
			return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
		}

		public async Task<IEnumerable<Transaction>> GetAllByUserAsync(Guid userId)
		{
			return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
		}

		public async Task AddAsync(Transaction transaction)
		{
			await _context.Transactions.AddAsync(transaction);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Transaction transaction)
		{
			_context.Transactions.Update(transaction);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Transaction transaction)
		{
			transaction.IsDeleted = true;
			_context.Transactions.Update(transaction);
			await _context.SaveChangesAsync();
		}

	}
}