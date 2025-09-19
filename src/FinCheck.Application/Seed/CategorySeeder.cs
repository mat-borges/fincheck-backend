using FinCheck.Domain.Models;

namespace FinCheck.Application.Seed;
public static class CategorySeeder
{
	public static List<Category> GetDefaultCategories(Guid userId)
	{
		return
        [
            new Category { Id = Guid.NewGuid(), Name = "Alimentação", Type = CategoryType.Expense, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Transporte", Type = CategoryType.Expense, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Moradia", Type = CategoryType.Expense, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Saúde", Type = CategoryType.Expense, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Lazer", Type = CategoryType.Expense, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Salário", Type = CategoryType.Income, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Investimentos", Type = CategoryType.Income, UserId = userId },
				new Category { Id = Guid.NewGuid(), Name = "Outros", Type = CategoryType.Income, UserId = userId }
		  ];
	}
}