using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.Models.Interfaces;

namespace Diary.Entities.Common
{
	public abstract class TrackableModify : ITrackableModify
	{
		public DateTime Created { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? Modified { get; set; }

		public string ModifiedBy { get; set; }
	}
}
