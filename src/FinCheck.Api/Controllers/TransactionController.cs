using System.Security.Claims;
using FinCheck.Application.DTOs.Transactions;
using FinCheck.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TransactionController(ITransactionService transactionService) : ControllerBase
	{
		private readonly ITransactionService _transactionService = transactionService;

		private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = GetUserId();
			var transactions = await _transactionService.GetAllAsync(userId);
			return Ok(transactions);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TransactionRequestDto dto)
		{
			var userId = GetUserId();
			var transaction = await _transactionService.CreateAsync(userId, dto);

			return CreatedAtAction(nameof(GetAll), new {id = transaction.Id}, transaction);
		}
	}
}