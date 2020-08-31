using System;

namespace Diary.Entities.DTOs.Account
{
	public class UserViewDTO
	{
		public Guid Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public Guid RoleId { get; set; }

		public string RoleName { get; set; }
	}
}
	