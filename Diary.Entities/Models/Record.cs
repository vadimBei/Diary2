using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;

namespace Diary.Entities.Models
{
	public class Record : TrackableModify
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public byte[] Text { get; set; }

		public Guid UserId { get; set; }

		public User User { get; set; }

		public byte[] IvKey { get; set; }

		public List<UploadedFile> UploadedFiles { get; set; }
	}
}
