using FinCheck.Domain.Models;
using FinCheck.Domain.Repositories;
using FinCheck.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinCheck.Infrastructure.Repositories
{
	public class AuthRepository(DataContextEF context) : IAuthRepository
	{
		private readonly DataContextEF _context = context;

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task AddUserAsync(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> EmailExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email == email);
		}

		public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
		}

		public async Task UpdateUserAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}
    }
}