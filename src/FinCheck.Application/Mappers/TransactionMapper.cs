using FinCheck.Application.DTOs.Transactions;
using FinCheck.Domain.Models;

namespace FinCheck.Application.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionResponseDto ToDto(Transaction t) =>
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
