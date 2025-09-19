using FinCheck.Application.DTOs.Accounts;
using FinCheck.Domain.Models;

namespace FinCheck.Application.Services.Interfaces
{
	public interface IAccountService
	{
		Task<List<Account>> GetAllAsync(Guid userId);
		Task<Account?> GetByIdAsync(Guid id);
		Task CreateAsync(Guid userId, AccountRequestDto account);
		Task UpdateBalanceAsync(Guid accountId, decimal ammount, TransactionType type);
    }
}