using System;

namespace Records.Core.Application.Common.Dtos
{
    public class RecordToUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] EncryptedContent { get; set; }
        public byte[] IvKey { get; set; }
    }
}
