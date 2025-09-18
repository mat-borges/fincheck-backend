using System.Security.Claims;
using Fincheck.Application.DTOs.Categories;
using Fincheck.Application.Services;
using Fincheck.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class CategoryController(CategoryService categoryService) : ControllerBase
	{
		private readonly CategoryService _categoryService = categoryService;

		private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = GetUserId();
			var categories = await _categoryService.GetAllAsync(userId);
			return Ok(categories);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoryRequestDto category)
		{
			var userId = GetUserId();
			await _categoryService.CreateAsync(userId, category);
			return CreatedAtAction(nameof(GetAll), new { userId }, category);
		}
	}
};