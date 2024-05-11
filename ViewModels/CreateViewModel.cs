using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
	public class CreateViewModel
	{
		[Required]
		public string FullName { get; set; } = string.Empty;


		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Password)]
		[MinLength(8)]
		public string Password { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Parola bilgileri eşleşmiyor.")] //Password değeri ile karşılaştırılmasını sağladık
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
