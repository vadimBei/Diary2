using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Diary.Entities.DTOs.Account
{
	public class UserRegisterDTO
	{
		[Required(ErrorMessage = "The field is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password mismatch")]
		public string PasswordConfirm { get; set; }

		public string Captcha { get; set; }
	}
}
