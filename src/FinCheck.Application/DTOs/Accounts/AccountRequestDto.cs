
namespace FinCheck.Application.DTOs.Accounts
{
    public partial class AccountRequestDto
    {
    	public string Name { get; set; } = string.Empty;

    	public decimal Balance { get; set; }
    }
}