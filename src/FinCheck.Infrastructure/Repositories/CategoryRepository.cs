using FinCheck.Domain.Models;
using FinCheck.Domain.Repositories;
using FinCheck.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinCheck.Infrastructure.Repositories
{
	public class CategoryRepository(DataContextEF context) : ICategoryRepository
	{
		private readonly DataContextEF _context = context;

		public async Task<Category?> GetByIdAsync(Guid id) => await _context.Categories.FindAsync(id);

		public async Task<List<Category>> GetAllByUserAsync(Guid userId) => await _context.Categories.Where(c => c.UserId == userId).ToListAsync();

		public async Task AddCategoryAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
		}

		public async Task AddRangeAsync(IEnumerable<Category> categories)
		{
			await _context.Categories.AddRangeAsync(categories);
			await _context.SaveChangesAsync();
		}

	}

}