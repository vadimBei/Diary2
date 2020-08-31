using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Entities.Models.Interfaces
{
	public interface ITrackable
	{
		DateTime Created { get; set; }
		string CreatedByName { get; set; }
		string CreatedByRole { get; set; }
	}
}
