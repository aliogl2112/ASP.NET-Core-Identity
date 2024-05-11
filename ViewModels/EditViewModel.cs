using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
	public class EditViewModel
	{
		public string? Id { get; set; }
		public string? FullName { get; set; } = string.Empty;

		[EmailAddress]
		public string? Email { get; set; } = string.Empty;

		[DataType(DataType.Password)]
		[MinLength(8)]
		public string? Password { get; set; } = string.Empty;

		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Parola bilgileri eşleşmiyor.")] //Password değeri ile karşılaştırılmasını sağladık
		public string? ConfirmPassword { get; set; } = string.Empty;

		public IList<string>? SelectedRoles { get; set; }
	}
}
