using System.ComponentModel.DataAnnotations;

namespace Route.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name ="First Name")]
		public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display (Name ="Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password" ,ErrorMessage ="Passwords Does Not Match!")]
		public string ConfirmPassword { get; set; } = null!;

		[Required]
		public bool IsAgree { get; set; }

	}
}
