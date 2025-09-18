using System.Security.Claims;
using Fincheck.Application.Services;
using Fincheck.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
	public class AccountController(AccountService accountService) : ControllerBase
	{
		private readonly AccountService _accountService = accountService;

		private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = GetUserId();
			var account = await _accountService.GetAllAsync(userId);
			return Ok(account);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Account account)
		{
			var userId = GetUserId();
			await _accountService.CreateAsync(userId, account);
			return CreatedAtAction(nameof(GetAll), new { userId = account.UserId }, account);
		}
	}
};