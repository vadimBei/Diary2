﻿using AutoMapper;
using Common.System.Interfaces;
using MediatR;
using Records.Core.Application.Common.Dtos;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Application.Common.ViewModels;
using Records.Core.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Records.Commands.CreateRecord
{
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, RecordVM>
    {
        private readonly IMapper _mapper;
        private readonly IRecordService _recordService;
        private readonly IAesCryptoProviderService _aesCryptoProviderService;

        public CreateRecordCommandHandler(
            IMapper mapper,
            IRecordService recordService,
            IAesCryptoProviderService aesCryptoProviderService)
        {
            _mapper = mapper;
            _recordService = recordService;
            _aesCryptoProviderService = aesCryptoProviderService;
        }

        public async Task<RecordVM> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            //string currentUserName = HttpContext.User.Identity.Name;
            //User user = await _userManager.FindByNameAsync(currentUserName);
            var user = new User();
            var ivKey = _aesCryptoProviderService.GenerateIV();
            //var encryptedContent = _aesCryptoProviderService.EncryptValue(request.Content, user.CryptoKey, ivKey);
            var recordDto = new RecordToCreateDto
            {
                IvKey = ivKey,
                Name = request.Name,
                UserId = user.UserId,
            };

            var record = await _recordService.CreateAsync(recordDto, cancellationToken);

            return _mapper.Map<RecordVM>(record);
        }
    }
}
