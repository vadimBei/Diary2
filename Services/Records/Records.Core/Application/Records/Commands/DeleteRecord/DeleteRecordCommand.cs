using MediatR;
using System;

namespace Records.Core.Application.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
