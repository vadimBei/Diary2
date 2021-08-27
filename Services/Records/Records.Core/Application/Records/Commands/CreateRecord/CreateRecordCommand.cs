using MediatR;
using Microsoft.AspNetCore.Http;
using Records.Core.Application.Common.ViewModels;

namespace Records.Core.Application.Records.Commands.CreateRecord
{
    public class CreateRecordCommand : IRequest<RecordVM>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsImage { get; set; }
        public IFormFileCollection NewFiles { get; set; }
    }
}
