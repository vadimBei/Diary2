using MediatR;
using Records.Core.Application.Common.ViewModels;
using System;
using System.Collections.Generic;

namespace Records.Core.Application.Records.Queries.GetUserRecords
{
    public class GetUserRecordsQuery : IRequest<List<RecordVM>>
    {
        public Guid UserId { get; set; }
    }
}
