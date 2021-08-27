using MediatR;
using Records.Core.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand>
    {
        private readonly IRecordService _recordService;

        public DeleteRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public async Task<Unit> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            await _recordService.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
