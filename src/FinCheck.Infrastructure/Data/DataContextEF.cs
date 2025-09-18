using Fincheck.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fincheck.Infrastructure.Data
{
    public class DataContextEF(IConfiguration config) : DbContext
    {
    	private readonly IConfiguration _config = config;

    	public virtual DbSet<User> Users { get; set; }
    	public virtual  DbSet<Account> Accounts { get; set; }
    	public virtual  DbSet<Category> Categories { get; set; }
    	public virtual  DbSet<Transaction> Transactions { get; set; }

    	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_config.GetConnectionString("Default"), opt =>
				{
					opt.EnableRetryOnFailure();
				});
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("fdbo");

			// Soft delete global filter
			modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
			modelBuilder.Entity<Account>().HasQueryFilter(a => !a.IsDeleted);
			modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
			modelBuilder.Entity<Transaction>().HasQueryFilter(t => !t.IsDeleted);

			// Relationships and cascading rules
			modelBuilder.Entity<User>()
					.HasMany(u => u.Accounts)
					.WithOne(a => a.User)
					.HasForeignKey(a => a.UserId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
					.HasMany(u => u.Categories)
					.WithOne(c => c.User)
					.HasForeignKey(c => c.UserId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
					.HasMany(u => u.Transactions)
					.WithOne(t => t.User)
					.HasForeignKey(t => t.UserId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Account>()
					.HasMany(a => a.Transactions)
					.WithOne(t => t.Account)
					.HasForeignKey(t => t.AccountId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Category>()
					.HasMany(c => c.Transactions)
					.WithOne(t => t.Category)
					.HasForeignKey(t => t.CategoryId)
					.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Account>()
					.HasIndex(a => new { a.UserId, a.Name })
					.IsUnique();

			modelBuilder.Entity<Transaction>()
					.Property(t => t.Type)
					.HasConversion<byte>();

			modelBuilder.Entity<Category>()
					.Property(c => c.Type)
					.HasConversion<byte>();
		}

	}
}