using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.DTOs.Record;

namespace Diary.Services.Interfaces
{
	public interface ICheckingAbilityToRemove
	{
		bool Check(DateTime created);
	}
}
