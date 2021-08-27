using MediatR;
using Records.Core.Application.Common.ViewModels;
using System;

namespace Records.Core.Application.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommand : IRequest<RecordVM>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
