using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Diary.Entities.DTOs.Account
{
	public class UserRegisterDTO
	{
		[Required(ErrorMessage = "The field is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
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
