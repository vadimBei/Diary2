using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.DTOs.Common;
using Diary.Entities.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Diary.Services
{
	public class SearchService : ISearchService
	{
		private readonly UserManager<User> _userManager;
		private readonly IRecordService _recordService;
		private readonly IHttpContextAccessor _accessor;

		public SearchService(
			IRecordService recordService,
			UserManager<User> userManager,
			IHttpContextAccessor accessor)
		{
			_recordService = recordService;
			_userManager = userManager;
			_accessor = accessor;
		}

		public async Task<IEnumerable<Record>> RecordsByCurrentUserAcync(SearchModel searchModel)
		{
			var minDate = DateTime.MinValue;

			var currentUserName = _accessor.HttpContext.User.Identity.Name;
			var user = await _userManager.FindByNameAsync(currentUserName);

			var recordsByCurrentUser = await _recordService.GetAllByUserIdAsync(user.Id);

			if (searchModel.StartDate != minDate && searchModel.EndDate != minDate)
			{
				recordsByCurrentUser = recordsByCurrentUser.Where(r => r.Created >= searchModel.StartDate && r.Created <= searchModel.EndDate);
			}
			else if (searchModel.EndDate != DateTime.MinValue)
			{
				recordsByCurrentUser = recordsByCurrentUser.Where(r => r.Created.Date <= searchModel.EndDate.Date);
			}
			else if (searchModel.StartDate != DateTime.MinValue)
			{
				recordsByCurrentUser = recordsByCurrentUser.Where(r => r.Created >= searchModel.StartDate);
			}

			return recordsByCurrentUser.OrderByDescending(d => d.Modified).ToList();
		}
	}
}
