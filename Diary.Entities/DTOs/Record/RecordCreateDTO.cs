using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Diary.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace Diary.Entities.DTOs.Record
{
	public class RecordCreateDTO : TrackableModify
	{
		[Required(ErrorMessage = "Поле не може бути пустим")]
		[MaxLength(50, ErrorMessage = "Перевищена максимальна кількість символів. Максимальна кількість символів: 50.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Поле не може бути пустим")]
		[MaxLength(500, ErrorMessage = "Перевищена максимальна кількість символів. Максимальна кількість символів: 500.")]
		public string Content { get; set; }

		public Guid UserId { get; set; }

		public bool IsImage { get; set; }

		public byte[] IvKey { get; set; }

		public IFormFileCollection NewFiles { get; set; }
		//public List<IFormFile> UploadedFiles { get; set; }
	}
}
