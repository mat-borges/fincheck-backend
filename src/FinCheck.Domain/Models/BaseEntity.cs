namespace FinCheck.Domain.Models
{
    public abstract class BaseEntity
    {
    	public Guid Id { get; set; }
    	public DateTime CreatedAt { get; set; }
    	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    	public bool IsDeleted { get; set; } = false; // Soft delete
    }
}