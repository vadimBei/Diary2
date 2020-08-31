using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Diary.Entities.DTOs.Account
{
	public class UserChangePasswordDTO : UserNewPasswordDTO
	{
		public string OldPassword { get; set; }
	}

	public class UserNewPasswordDTO
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Password mismatch")]
		public string NewPasswordConfirm { get; set; }

		public string Email { get; set; }
	}
}
