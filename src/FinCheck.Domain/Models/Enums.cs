
namespace Fincheck.Domain.Models
{
    public enum TransactionType : byte
    {
        Income = 1,
        Expense = 2,
        Transfer = 3,
        Fee = 4
    }
}