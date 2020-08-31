using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Diary.Repository;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class UserService : BaseService<User>, IUserService
	{
		private Repository<User> _repository;

		public UserService(DiaryContext diaryContext)
			: base(diaryContext)
		{
			_repository = new Repository<User>(diaryContext);
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return (await _repository.FindAllAsync());
		}

		public override async Task<User> GetByIdAsync(Guid id)
		{
			return (await _repository.FindByAsync(u => u.Id == id)).FirstOrDefault();
		}

	}
}
