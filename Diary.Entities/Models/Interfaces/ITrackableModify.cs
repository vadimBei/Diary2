﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Entities.Models.Interfaces
{
	public interface ITrackableModify
	{
		DateTime Created { get; set; }
		string CreatedBy { get; set; }
		DateTime? Modified { get; set; }
		string ModifiedBy { get; set; }
	}
}
