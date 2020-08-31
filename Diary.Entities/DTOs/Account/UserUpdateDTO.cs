using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Diary.Entities.Models;

namespace Diary.Entities.DTOs.Account
{
	public class UserUpdateDTO
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[MaxLength(50, ErrorMessage = "The length can't be more than 50.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "The field is required")]
		[MaxLength(50, ErrorMessage = "The length can't be more than 50.")]
		[EmailAddress]
		public string Email { get; set; }

		public List<string> RolesInCurrentUser { get; set; }

		public List<AppRole> AllRoles { get; set; }

	}
}
