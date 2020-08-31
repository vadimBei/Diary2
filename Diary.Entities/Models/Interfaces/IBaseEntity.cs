using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Entities.Models.Interfaces
{
	public interface IBaseEntity
	{
		DateTime CreationData { get; set; }

		DateTime ModifiedDate { get; set; }
	}
}
