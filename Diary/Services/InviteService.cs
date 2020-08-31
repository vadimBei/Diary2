using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Diary.Repository;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class InviteService : BaseService<Invite>, IInviteService
	{
		private Repository<Invite> _repository;

		public InviteService(DiaryContext diaryContext)
			: base(diaryContext)
		{
			_repository = new Repository<Invite>(diaryContext);
		}

		public override async Task<Invite> GetByIdAsync(Guid id)
		{
			return (await _repository.FindByAsync(u => u.Id == id)).FirstOrDefault();
		}
	}
}
