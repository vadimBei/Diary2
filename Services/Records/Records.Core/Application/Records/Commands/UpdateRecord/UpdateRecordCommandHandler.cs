using AutoMapper;
using Common.System.Interfaces;
using MediatR;
using Records.Core.Application.Common.Dtos;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Application.Common.ViewModels;
using Records.Core.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, RecordVM>
    {
        private readonly IMapper _mapper;
        private readonly IRecordService _recordService;
        private readonly IAesCryptoProviderService _aesCryptoProviderService;

        public UpdateRecordCommandHandler(
            IMapper mapper,
            IRecordService recordService,
            IAesCryptoProviderService aesCryptoProviderService)
        {
            _mapper = mapper;
            _recordService = recordService;
            _aesCryptoProviderService = aesCryptoProviderService;
        }

        public async Task<RecordVM> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var ivKey = _aesCryptoProviderService.GenerateIV();

            var recordDto = new RecordToUpdateDto
            {
                Id = request.Id,
                //EncryptedContent = _aesCryptoProviderService.EncryptValue(request.Content, user.CryptoKey, ivKey),
                Name = request.Name,
                IvKey = ivKey
            };

            var record = await _recordService.UpdateAsync(recordDto, cancellationToken);
            return _mapper.Map<RecordVM>(record);
        }
    }
}
