using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Entities.DTOs.Common
{
	public class SearchModel
	{
		public SearchModel()
		{
		}

		public SearchModel(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

	}
}
