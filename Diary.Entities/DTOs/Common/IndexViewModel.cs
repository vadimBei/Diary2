using System;
using System.Collections.Generic;
using System.Text;
using Diary.Entities.DTOs.Record;

namespace Diary.Entities.DTOs.Common
{
	public class IndexViewModel
	{
		public IEnumerable<RecordViewDTO> RecordViewDTOs { get; set; }

		public PagingModel PagingModel { get; set; }

		public SearchModel SearchModel { get; set; }
	}
}
