using MediatR;
using Microsoft.AspNetCore.Mvc;
using Records.Core.Application.Common.ViewModels;
using Records.Core.Application.Records.Commands.CreateRecord;
using Records.Core.Application.Records.Commands.UpdateRecord;
using Records.Core.Application.Records.Queries.GetRecordById;
using System;
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

        [Route("getById/{recordId}")]
        [HttpGet]
        public async Task<RecordVM> GetRecordById(Guid recordId)
        {
            return await _mediator.Send(new GetRecordByIdQuery(recordId));
        }

        [Route("update")]
        [HttpPost]
        public async Task<RecordVM> Update(UpdateRecordCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
