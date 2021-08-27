using Microsoft.AspNetCore.Http;
using System;

namespace Records.Core.Application.Common.Dtos
{
    public class RecordToCreateDto
    {
		public string Name { get; set; }

		public byte[] EncryptedContent { get; set; }

		public Guid UserId { get; set; }

		public bool IsImage { get; set; }

		public byte[] IvKey { get; set; }

		public IFormFileCollection NewFiles { get; set; }
	}
}
