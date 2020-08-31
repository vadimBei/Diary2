using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.DTOs.Record;

namespace Diary.Entities.DTOs.UploadedFile
{
	public class UploadedFileViewDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public bool IsImage { get; set; }

		public Guid RecordId { get; set; }

		public bool Delete { get; set; }

		public RecordViewDTO RecordViewDTO { get; set; }
	}
}
