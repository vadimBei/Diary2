using MediatR;
using Microsoft.AspNetCore.Mvc;
using Records.Core.Application.Common.ViewModels;
using Records.Core.Application.Records.Commands.CreateRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Records.API.Controllers
{
    [Route("/api/records")]
    public class RecordsController : Controller
    {
        private readonly IMediator _mediator;

        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("create")]
        [HttpPost]
        public async Task<RecordVM> CreateRecord(CreateRecordCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
