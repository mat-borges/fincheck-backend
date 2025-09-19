using FinCheck.Application.DTOs.Transactions;
using FinCheck.Application.Mappers;
using FinCheck.Application.Services.Interfaces;
using FinCheck.Domain.Models;
using FinCheck.Domain.Repositories;

namespace FinCheck.Application.Services
{
	public class TransactionService(ITransactionRepository transactionRepository, IAccountService accountService) : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository = transactionRepository;
		private readonly IAccountService _accountService = accountService;

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

			return TransactionMapper.ToDto(transaction);
		}

		public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId)
		{
			var transactions = await _transactionRepository.GetAllByUserAsync(userId);
			return transactions.Select(TransactionMapper.ToDto);
		}

		public async Task<TransactionResponseDto?> GetByIdAsync(Guid id, Guid userId)
		{
			var transaction = await _transactionRepository.GetByIdAsync(id, userId);
			return transaction == null ? null : TransactionMapper.ToDto(transaction);
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

			return TransactionMapper.ToDto(transaction);
		}

		public async Task<bool> DeleteAsync(Guid id, Guid userId)
		{
			var transaction = await _transactionRepository.GetByIdAsync(id, userId);
			if (transaction == null) return false;

			await _transactionRepository.DeleteAsync(transaction);
			return true;
		}
	}
}
