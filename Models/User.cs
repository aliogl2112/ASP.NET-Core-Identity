using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
	public class User:IdentityUser
	{
		public string? FullName { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
	}
}
