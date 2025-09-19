using FinCheck.Application.DTOs.Transactions;
using FinCheck.Domain.Models;

namespace FinCheck.Application.Services.Interfaces
{
	public interface ITransactionService
	{
		Task<TransactionResponseDto> CreateAsync(Guid userId, TransactionRequestDto dto);

		Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId);

		Task<TransactionResponseDto?> GetByIdAsync(Guid id, Guid userId);

		Task<TransactionResponseDto?> UpdateAsync(Guid userId, TransactionUpdateDto dto);

		Task<bool> DeleteAsync(Guid id, Guid userId);
	}
}
