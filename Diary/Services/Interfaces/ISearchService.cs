using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.DTOs.Common;
using Diary.Entities.Models;

namespace Diary.Services.Interfaces
{
	public interface ISearchService
	{
		Task<IEnumerable<Record>> RecordsByCurrentUserAcync(SearchModel searchModel);
	}
}
