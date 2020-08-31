using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Common;
using Diary.Entities.DTOs.Account;
using Diary.Entities.DTOs.UploadedFile;
using Microsoft.AspNetCore.Http;

namespace Diary.Entities.DTOs.Record
{
	public class RecordViewDTO : TrackableModify
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public string DecryptedText { get; set; }

		public Guid UserId { get; set; }

		public UserViewDTO UserViewDTO { get; set; }

		public bool IsImage { get; set; }

		public byte[] IvKey { get; set; }

		public List<UploadedFileViewDTO> UploadedFileViewDTOs { get; set; }
	}
}
