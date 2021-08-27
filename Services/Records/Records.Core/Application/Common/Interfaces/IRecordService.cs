using Common.System.Interfaces;
using Records.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Records.Core.Application.Common.Interfaces
{
    public interface IRecordService : IBaseService<Record>
	{
		//Task<IEnumerable<Record>> GetAllByUserIdAsync(Guid userId);

		Task UpdateEntryAsync(Record record);
	}
}
