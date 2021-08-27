using System;

namespace Common.System.Models
{
    public abstract class AuditableEntity<T>
    {
        public T CreatedBy { get; set; }
        public DateTime DateOfCreation { get; set; }
        public T LastModifiedBy { get; set; }
        public DateTime? DateOfModification { get; set; }
    }
}
