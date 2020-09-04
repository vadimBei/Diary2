using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Diary.Entities.Models;
using AutoMapper;
using Diary.Entities.DTOs.Common;
using Diary.Entities.DTOs.Record;

namespace Diary.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAesCryptoProviderService _aesCryptoProvider;
		private readonly UserManager<User> _userManager;
		private readonly ISearchService _searchService;
		private readonly IMapper _mapper;

		public HomeController(
			IAesCryptoProviderService aesCryptoProvider,
			UserManager<User> userManager,
			ISearchService searchService,
			IMapper mapper)
		{
			_aesCryptoProvider = aesCryptoProvider;
			_searchService = searchService;
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(string searchString, DateTime startDate, DateTime endDate, int page = 1)
		{
			string currentUserName = HttpContext.User.Identity.Name;

			User user = await _userManager.FindByNameAsync(currentUserName);

			SearchModel search = new SearchModel(startDate, endDate);

			IEnumerable<Record> recordsByCurrentUser = await _searchService.RecordsByCurrentUserAcync(search);

			List<RecordViewDTO> decryptedRecords = new List<RecordViewDTO>();

			foreach (var record in recordsByCurrentUser)
			{
				string decriptedText = _aesCryptoProvider.DecryptValue(record.Text, user.CryptoKey, record.IvKey);

				if (decriptedText.Length > 200)
				{
					decriptedText = decriptedText.Substring(0, 200);
				}


				var decryptedRecord = _mapper.Map<RecordViewDTO>(record);
				decryptedRecord.DecryptedText = decriptedText;

				// Checking if a record can be deleted
				DateTime today = DateTime.Now;

				System.TimeSpan timeSpan =	today.Subtract(decryptedRecord.Created);

				var days = timeSpan.Days;

				if (days > 2)
					decryptedRecord.AbilityToRemove = false;
				else
					decryptedRecord.AbilityToRemove = true;

				decryptedRecords.Add(decryptedRecord);
			}

			//string search
			if (!string.IsNullOrEmpty(searchString))
			{
				decryptedRecords = decryptedRecords.Where(r => r.DecryptedText.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

				if (decryptedRecords.Count == 0)
					return PartialView("NoResults");
			}

			//number records on page
			int pageSize = 5;
			var count = decryptedRecords.Count();

			var listToDisplay = decryptedRecords.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			PagingModel pagingModel = new PagingModel(count, page, pageSize);

			IndexViewModel indexViewModel = new IndexViewModel
			{
				PagingModel = pagingModel,
				RecordViewDTOs = listToDisplay,
				SearchModel = search
			};

			if (listToDisplay.Count() == 0)
				indexViewModel.DisplayPagination = false;
			else
				indexViewModel.DisplayPagination = true;

			return View(indexViewModel);
		}
	}
}
