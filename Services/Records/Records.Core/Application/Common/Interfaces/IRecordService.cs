using Records.Core.Application.Common.Dtos;
using Records.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Common.Interfaces
{
    public interface IRecordService 
	{
		//Task<IEnumerable<Record>> GetAllByUserIdAsync(Guid userId);
		Task<Record> GetByIdAsync(Guid id, CancellationToken cancellationToken);
		Task<Record> CreateAsync(RecordToCreateDto recordDto, CancellationToken cancellationToken);
		Task<Record> UpdateAsync(RecordToUpdateDto recordDto, CancellationToken cancellationToken);
	}
}
