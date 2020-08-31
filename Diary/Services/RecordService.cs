using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Diary.Repository;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class RecordService : BaseService<Record>, IRecordService
	{
		private Repository<Record> _repository;

		public RecordService(DiaryContext diaryContext)
			: base(diaryContext)
		{
			_repository = new Repository<Record>(diaryContext);
		}

		public async Task<IEnumerable<Record>> GetAllByUserIdAsync(Guid userId)
		{
			//var records = await _repository.FindByConditionAsync(u => u.UserId == userId);
			//return records.OrderByDescending(d => d.Modified).ToList();
			return (await _repository.FindByConditionAsync(u => u.UserId == userId));
		}

		public override async Task<Record> GetByIdAsync(Guid id)
		{
			return (await _repository.FindByAsync(u => u.Id == id, f=>f.UploadedFiles)).FirstOrDefault();
		}

		public async Task UpdateEntryAsync(Record record)
		{
			await _repository.UpdateEntryAsync(record, r => r.Name, r => r.Text);
		}
	}
}
