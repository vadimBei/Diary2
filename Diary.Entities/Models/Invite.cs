using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;

namespace Diary.Entities.Models
{
	public class Invite : TrackableModify
	{
		public Guid Id { get; set; }

		public string EmailNewUser { get; set; }

		public bool Disabled { get; set; }
	}
}
