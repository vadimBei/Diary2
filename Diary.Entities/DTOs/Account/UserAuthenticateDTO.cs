using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Diary.Entities.DTOs.Account
{
	public class UserAuthenticateDTO
	{
		[Required(ErrorMessage = "The field is required")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Запомнить?")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }
	}
}
