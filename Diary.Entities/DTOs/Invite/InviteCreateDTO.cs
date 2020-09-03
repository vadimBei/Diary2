using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using System.Web.Mvc;
using Diary.Entities.Common;

namespace Diary.Entities.DTOs.Invite
{
	public class InviteCreateDTO : TrackableModify
	{
		[Required]
		public string EmailNewUser { get; set; }

		[Required]
		public string Message { get; set; }

		public bool Disabled { get; set; }
	}
}
