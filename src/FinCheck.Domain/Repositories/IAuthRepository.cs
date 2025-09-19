using FinCheck.Domain.Models;

namespace FinCheck.Domain.Repositories
{
	public interface IAuthRepository
	{
		Task<User?> GetUserByEmailAsync(string email);
		Task AddUserAsync(User user);
		Task<bool> EmailExistsAsync(string email);
		Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
		Task UpdateUserAsync(User user);
    }
}