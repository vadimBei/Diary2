using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;

namespace Diary.Services.Interfaces
{
	public interface IRecordService : IBaseService<Record>
	{
		Task<IEnumerable<Record>> GetAllByUserIdAsync(Guid userId);

		Task UpdateEntryAsync(Record record);
	}
}
