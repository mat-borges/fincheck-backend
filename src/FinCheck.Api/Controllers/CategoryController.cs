using System.Security.Claims;
using FinCheck.Application.DTOs.Categories;
using FinCheck.Application.Services;
using FinCheck.Application.Services.Interfaces;
using FinCheck.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCheck.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class CategoryController(ICategoryService categoryService) : ControllerBase
	{
		private readonly ICategoryService _categoryService = categoryService;

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