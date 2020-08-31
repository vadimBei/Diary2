using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;

namespace Diary.Entities.Models
{
	public class UploadedFile : TrackableModify
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public bool IsImage { get; set; }

		public Guid RecordId { get; set; }

		public Record Record { get; set; }
	}
}
