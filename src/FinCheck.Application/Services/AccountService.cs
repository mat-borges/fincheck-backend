using Fincheck.Application.DTOs.Accounts;
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

		public async Task CreateAsync(Guid userId, AccountRequestDto account)
		{
			var newAccount = new Account
			{
				UserId = userId,
				Name = account.Name,
				Balance = account.Balance
			};
			await _accountRepository.AddAccountAsync(newAccount);
		}

		public async Task UpdateBalanceAsync(Guid accountId, decimal ammount, TransactionType type)
		{
			var account = await _accountRepository.GetByIdAsync(accountId) ?? throw new Exception("Account not found");

			switch (type)
			{
				case TransactionType.Income:
					account.Balance += ammount;
					break;
				case TransactionType.Expense:
					account.Balance -= ammount;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			account.UpdatedAt = DateTime.UtcNow;
			await _accountRepository.UpdateAsync(account);
      }

    }
}