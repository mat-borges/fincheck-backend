using Fincheck.Domain.Models;
using FinCheck.Infrastructure.Repositories;

namespace Fincheck.Application.Services
{
	public class AccountService(AccountRepository accountRepository)
	{
		private readonly AccountRepository _accountRepository = accountRepository;

		public async Task<List<Account>> GetAllAsync(Guid userId)
		{
			return await _accountRepository.GetAllByUserAsync(userId);
		}

		public async Task<Account?> GetByIdAsync(Guid id)
		{
			return await _accountRepository.GetByIdAsync(id);
		}

		public async Task CreateAsync(Guid userId, Account account)
		{
			account.UserId = userId;
			await _accountRepository.AddAccountAsync(account);
		}
	 }
}