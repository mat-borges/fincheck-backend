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

		public async Task CreateAsync(Guid userId, Category category)
		{
			category.UserId = userId;
			await _categoryRepository.CreateAsync(category);
		}

		public async Task AddRangeAsync(IEnumerable<Category> categories)
		{
			await _categoryRepository.AddRangeAsync(categories);
		}
	 }
}