using FinCheck.Application.DTOs.Categories;
using FinCheck.Application.Services.Interfaces;
using FinCheck.Domain.Models;
using FinCheck.Domain.Repositories;

namespace FinCheck.Application.Services
{
	public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository = categoryRepository;

		public async Task<List<Category>> GetAllAsync(Guid userId)
		{
			return await _categoryRepository.GetAllByUserAsync(userId);
		}

		public async Task CreateAsync(Guid userId, CategoryRequestDto category)
		{
			var newCategory = new Category
			{
				UserId = userId,
				Name = category.Name,
				Type = category.Type,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _categoryRepository.AddCategoryAsync(newCategory);
		}

		public async Task AddRangeAsync(IEnumerable<Category> categories)
		{
			await _categoryRepository.AddRangeAsync(categories);
		}
	 }
}