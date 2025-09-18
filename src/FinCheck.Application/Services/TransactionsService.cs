using Fincheck.Application.DTOs.Transactions;
using Fincheck.Domain.Models;
using Fincheck.Infrastructure.Repositories;

namespace Fincheck.Application.Services
{
	public class TransactionService(TransactionRepository transactionRepository, AccountService accountService)
	{
		private readonly TransactionRepository _transactionRepository = transactionRepository;
		private readonly AccountService _accountService = accountService;

		public async Task<TransactionResponseDto> CreateAsync(Guid userId, TransactionRequestDto dto)
		{
			var transaction = new Transaction
			{
				UserId = userId,
				AccountId = dto.AccountId,
				CategoryId = dto.CategoryId,
				Title = dto.Title,
				Description = dto.Description,
				Amount = dto.Amount,
				TransactionDate = dto.TransactionDate,
				Type = dto.Type,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			await _transactionRepository.AddAsync(transaction);

			var account = await _accountService.GetByIdAsync(dto.AccountId) ?? throw new Exception("Account not found");
			await _accountService.UpdateBalanceAsync(account.Id, dto.Amount, dto.Type);

			return MapToDto(transaction);
		}

		public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId)
		{
			var transactions = await _transactionRepository.GetAllByUserAsync(userId);
			return transactions.Select(MapToDto);
		}

		public async Task<TransactionResponseDto?> GetByIdAsync(Guid id, Guid userId)
		{
			var transaction = await _transactionRepository.GetByIdAsync(id, userId);
			return transaction == null ? null : MapToDto(transaction);
		}

		public async Task<TransactionResponseDto?> UpdateAsync(Guid userId, TransactionUpdateDto dto)
		{
			var transaction = await _transactionRepository.GetByIdAsync(dto.Id, userId);
			if (transaction == null) return null;

			transaction.AccountId = dto.AccountId;
			transaction.CategoryId = dto.CategoryId;
			transaction.Title = dto.Title;
			transaction.Description = dto.Description;
			transaction.Amount = dto.Amount;
			transaction.TransactionDate = dto.TransactionDate;
			transaction.Type = dto.Type;
			transaction.UpdatedAt = DateTime.UtcNow;

			await _transactionRepository.UpdateAsync(transaction);

			return MapToDto(transaction);
		}

		public async Task<bool> DeleteAsync(Guid id, Guid userId)
		{
			var transaction = await _transactionRepository.GetByIdAsync(id, userId);
			if (transaction == null) return false;

			await _transactionRepository.DeleteAsync(transaction);
			return true;
		}

		private static TransactionResponseDto MapToDto(Transaction t) =>
				new()
				{
					Id = t.Id,
					AccountId = t.AccountId,
					CategoryId = t.CategoryId,
					Title = t.Title,
					Description = t.Description,
					Amount = t.Amount,
					TransactionDate = t.TransactionDate,
					Type = t.Type
				};

	}
}
