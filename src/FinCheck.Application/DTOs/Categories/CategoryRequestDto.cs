using FinCheck.Domain.Models;

namespace FinCheck.Application.DTOs.Categories
{
    public partial class CategoryRequestDto
    {
    	public string Name { get; set; } = string.Empty;
    	public CategoryType Type { get; set; }
    }
}