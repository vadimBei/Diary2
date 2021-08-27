using Common.System.Models;
using System;

namespace Records.Core.Domain.Entities
{
    public class Record : AuditableEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Text { get; set; }

        public Guid UserId { get; set; }

        public byte[] IvKey { get; set; }

        public bool IsDeleted { get; set; }
    }
}
