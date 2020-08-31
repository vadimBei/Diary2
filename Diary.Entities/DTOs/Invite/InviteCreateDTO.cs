using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;

namespace Diary.Entities.DTOs.Invite
{
	public class InviteCreateDTO : TrackableModify
	{
		public string EmailNewUser { get; set; }

		public string Message { get; set; }

		public bool Disabled { get; set; }
	}
}
