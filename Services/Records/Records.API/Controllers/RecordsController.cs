using MediatR;
using Microsoft.AspNetCore.Mvc;
using Records.Core.Application.Common.ViewModels;
using Records.Core.Application.Records.Commands.CreateRecord;
using Records.Core.Application.Records.Commands.DeleteRecord;
using Records.Core.Application.Records.Commands.UpdateRecord;
using Records.Core.Application.Records.Queries.GetRecordById;
using Records.Core.Application.Records.Queries.GetUserRecords;
using System;
using System.Collections.Generic;
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

        [HttpPost("create")]
        public async Task<RecordVM> CreateRecord(CreateRecordCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("getById/{id}")]
        public async Task<RecordVM> GetRecordById(Guid id)
        {
            return await _mediator.Send(new GetRecordByIdQuery
            {
                RecordId = id
            });
        }

        [HttpPut("update")]
        public async Task<RecordVM> Update(UpdateRecordCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRecordCommand
            {
                Id = id
            });

            return NoContent();
        }

        [HttpGet("getAllByUserId/{userId}")]
        public async Task<List<RecordVM>> GetUserRecords(Guid userId)
        {
            return await _mediator.Send(new GetUserRecordsQuery
            {
                UserId = userId
            });
        }
    }
}
