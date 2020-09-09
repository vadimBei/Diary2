using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Diary.Entities.DTOs.UploadedFile;
using Microsoft.AspNetCore.Http;

namespace Diary.Entities.DTOs.Record
{
	public class RecordUpdateDTO
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Поле не може бути пустим")]
		[MaxLength(50, ErrorMessage = "Перевищена максимальна кількість символів. Максимальна кількість символів: 50.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Поле не може бути пустим")]
		[MaxLength(500, ErrorMessage = "Перевищена максимальна кількість символів. Максимальна кількість символів: 500.")]
		public string DecryptedText { get; set; }

		public Guid UserId { get; set; }

		public List<UploadedFileViewDTO> UploadedFileViewDTOs { get; set; }

		public IFormFileCollection NewFiles { get; set; }

		public bool AbilityToRemove { get; set; }

	}
}
