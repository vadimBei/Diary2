using MediatR;
using Records.Core.Application.Common.ViewModels;
using System;

namespace Records.Core.Application.Records.Queries.GetRecordById
{
    public class GetRecordByIdQuery : IRequest<RecordVM>
    {
        public Guid RecordId { get; set; }
    }
}
