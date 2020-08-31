using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Diary.Entities.Models
{
	public class User : IdentityUser<Guid>, ITrackableModify
	{
		public List<Record> Records { get; set; }

		public DateTime? DeletedDate { get; set; }

		public byte[] CryptoKey { get; set; }

		public DateTime Created { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? Modified { get; set; }

		public string ModifiedBy { get; set; }

	}
}
