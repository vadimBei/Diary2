using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Diary.Repository;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class UploadedFileService : BaseService<UploadedFile>, IUploadedFileService
	{
		private Repository<UploadedFile> _repository;

		public UploadedFileService(DiaryContext diaryContext)
			: base(diaryContext)
		{
			_repository = new Repository<UploadedFile>(diaryContext);
		}

		public override async Task<UploadedFile> GetByIdAsync(Guid id)
		{
			return (await _repository.FindByAsync(u => u.Id == id)).FirstOrDefault();
		}
	}
}
