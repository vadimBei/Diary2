using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;

namespace Diary.Entities.DTOs.UploadedFile
{
	public class UploadedFileCreateDTO : TrackableModify
	{
		public string Name { get; set; }

		public string Path { get; set; }

		public bool IsImage { get; set; }

		public Guid RecordId { get; set; }
	}
}
