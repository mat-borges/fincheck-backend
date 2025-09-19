using System.Security.Claims;
using FinCheck.Application.DTOs.Accounts;
using FinCheck.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class AccountController(IAccountService accountService) : ControllerBase
	{
		private readonly IAccountService _accountService = accountService;

		private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = GetUserId();
			var account = await _accountService.GetAllAsync(userId);
			return Ok(account);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AccountRequestDto account)
		{
			var userId = GetUserId();
			await _accountService.CreateAsync(userId, account);
			return CreatedAtAction(nameof(GetAll), new { userId }, account);
		}
	}
};