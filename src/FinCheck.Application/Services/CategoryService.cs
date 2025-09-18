using Fincheck.Application.DTOs.Categories;
using Fincheck.Domain.Models;
using FinCheck.Infrastructure.Repositories;

namespace Fincheck.Application.Services
{
	public class CategoryService(CategoryRepository categoryRepository)
	{
		private readonly CategoryRepository _categoryRepository = categoryRepository;

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