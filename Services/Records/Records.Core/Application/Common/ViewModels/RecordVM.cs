using AutoMapper;
using Records.Core.Domain.Entities;
using System;

namespace Records.Core.Application.Common.ViewModels
{
    [AutoMap(typeof(Record))]
    public class RecordVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Text { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfModification { get; set; }
    }
}
