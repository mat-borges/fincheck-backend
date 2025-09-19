using FinCheck.Application.DTOs.Transactions;
using FinCheck.Application.Services;
using FinCheck.Application.Services.Interfaces;
using FinCheck.Domain.Models;
using FinCheck.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace FinCheck.Tests.Unit.Services
{
	public class TransactionServiceTests
	{
		private readonly Mock<ITransactionRepository> _transactionRepoMock;
		private readonly Mock<IAccountService> _accountServiceMock;
		private readonly TransactionService _transactionService;

		public TransactionServiceTests()
		{
			_transactionRepoMock = new Mock<ITransactionRepository>();
			_accountServiceMock = new Mock<IAccountService>();
			_transactionService = new TransactionService(_transactionRepoMock.Object, _accountServiceMock.Object);
		}

		[Fact]
		public async Task CreateAsync_ShouldAddTransaction_AndUpdateAccountBalance()
		{
			var accountId = Guid.NewGuid();
			var userId = Guid.NewGuid();
			var account = new Account { Id = accountId, UserId = userId, Balance = 100 };

			_accountServiceMock.Setup(a => a.GetByIdAsync(accountId)).ReturnsAsync(account);


			var dto = new TransactionRequestDto
			{
				AccountId = accountId,
				CategoryId = Guid.NewGuid(),
				Title = "[Test] Salary",
				Description = "This is a test transaction",
				Amount = 2000,
				TransactionDate = DateTime.UtcNow,
				Type = TransactionType.Income
			};

			var result = await _transactionService.CreateAsync(userId, dto);

			result.Should().NotBeNull();
			result.Title.Should().Be("[Test] Salary");
			result.Amount.Should().Be(2000);
			result.Type.Should().Be(TransactionType.Income);

			_transactionRepoMock.Verify(r => r.AddAsync(It.Is<Transaction>(t =>
				t.AccountId == dto.AccountId &&
				t.Amount == dto.Amount &&
				t.Type == TransactionType.Income
			)), Times.Once);

			_accountServiceMock.Verify(a => a.UpdateBalanceAsync(accountId, dto.Amount, TransactionType.Income), Times.Once);
		}

	}
}