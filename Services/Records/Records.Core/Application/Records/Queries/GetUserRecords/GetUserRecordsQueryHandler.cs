using AutoMapper;
using MediatR;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Application.Common.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Records.Queries.GetUserRecords
{
    public class GetUserRecordsQueryHandler : IRequestHandler<GetUserRecordsQuery, List<RecordVM>>
    {
        private readonly IMapper _mapper;
        private readonly IRecordService _recordService;

        public GetUserRecordsQueryHandler(
            IMapper mapper,
            IRecordService recordService)
        {
            _mapper = mapper;
            _recordService = recordService;
        }

        public async Task<List<RecordVM>> Handle(GetUserRecordsQuery request, CancellationToken cancellationToken)
        {
            var userRecords = await _recordService.GetAllByUserIdAsync(request.UserId);

            return _mapper.Map<List<RecordVM>>(userRecords);
        }
    }
}
