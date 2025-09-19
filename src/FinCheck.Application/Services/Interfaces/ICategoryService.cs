using FinCheck.Application.DTOs.Categories;
using FinCheck.Domain.Models;

namespace FinCheck.Application.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<List<Category>> GetAllAsync(Guid userId);

		Task CreateAsync(Guid userId, CategoryRequestDto category);

		Task AddRangeAsync(IEnumerable<Category> categories);
	 }
}