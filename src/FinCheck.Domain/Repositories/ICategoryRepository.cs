using FinCheck.Domain.Models;

namespace FinCheck.Domain.Repositories
{
	public interface ICategoryRepository
	{
		Task<Category?> GetByIdAsync(Guid id);
		Task<List<Category>> GetAllByUserAsync(Guid userId);
		Task AddCategoryAsync(Category category);
		Task AddRangeAsync(IEnumerable<Category> categories);
	}

}