using AutoMapper;
using MediatR;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Application.Common.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Records.Queries.GetRecordById
{
    public class GetRecordByIdQueryHandler : IRequestHandler<GetRecordByIdQuery, RecordVM>
    {
        private readonly IMapper _mapper;
        private readonly IRecordService _recordService;
        public GetRecordByIdQueryHandler(
            IMapper mapper,
            IRecordService recordService)
        {
            _mapper = mapper;
            _recordService = recordService;
        }

        public async Task<RecordVM> Handle(GetRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetByIdAsync(request.RecordId, cancellationToken);

            return _mapper.Map<RecordVM>(record);
        }
    }
}
