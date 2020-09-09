using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Entities.DTOs.Record;
using Diary.Entities.DTOs.UploadedFile;
using Diary.Entities.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Diary.Controllers
{
	[Authorize]
	public class RecordController : Controller
	{
		private readonly ICheckingAbilityToRemove _checkingAbilityToRemove;
		private readonly IAesCryptoProviderService _aesCryptoProvider;
		private readonly IUploadedFileService _uploadedFileService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UserManager<User> _userManager;
		private readonly IRecordService _recordService;
		private readonly IMapper _mapper;

		public RecordController(
			ICheckingAbilityToRemove checkingAbilityToRemove,
			IAesCryptoProviderService aesCryptoProvider,
			IUploadedFileService uploadedFileService,
			IWebHostEnvironment webHostEnvironment,
			UserManager<User> userManager,
			IRecordService recordService,
			IMapper mapper
			)
		{
			_checkingAbilityToRemove = checkingAbilityToRemove;
			_uploadedFileService = uploadedFileService;
			_webHostEnvironment = webHostEnvironment;
			_aesCryptoProvider = aesCryptoProvider;
			_recordService = recordService;
			_userManager = userManager;
			_mapper = mapper;
		}

		public IActionResult CreateRecord()
		{
			return View();
		}

		[HttpPost]
		[DisableRequestSizeLimit]
		public async Task<IActionResult> CreateRecord(RecordCreateDTO recordCreateDTO)
		{
			if (!ModelState.IsValid)
			{
				return View(recordCreateDTO);
			}

			try
			{
				string currentUserName = HttpContext.User.Identity.Name;

				User user = await _userManager.FindByNameAsync(currentUserName);
				byte[] ivKey = _aesCryptoProvider.GenerateIV();

				recordCreateDTO.Created = DateTime.Now;
				recordCreateDTO.Modified = DateTime.Now;
				recordCreateDTO.IvKey = ivKey;
				recordCreateDTO.UserId = user.Id;

				Record record = _mapper.Map<Record>(recordCreateDTO);

				record.Text = _aesCryptoProvider.EncryptValue(recordCreateDTO.Content, user.CryptoKey, ivKey);

				Record newRecord = await _recordService.CreateAsync(record);

				if (recordCreateDTO.NewFiles != null)
				{
					List<UploadedFile> uploadedFiles =
						AddFiles(recordCreateDTO.NewFiles, newRecord.Id, currentUserName);

					foreach (var file in uploadedFiles)
						await _uploadedFileService.CreateAsync(file);
				}

				return RedirectToAction("Index", "Home");
			}
			catch (DbUpdateException ex)
			{
				ModelState.AddModelError("", ex.Message);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return View(recordCreateDTO);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Record record = await _recordService.GetByIdAsync(id);

			User user = await _userManager.FindByIdAsync(record.UserId.ToString());

			RecordUpdateDTO recordViewDTO = _mapper.Map<RecordUpdateDTO>(record);

			recordViewDTO.DecryptedText = _aesCryptoProvider.DecryptValue(record.Text, user.CryptoKey, record.IvKey);

			recordViewDTO.AbilityToRemove = _checkingAbilityToRemove.Check(record.Created);

			return View(recordViewDTO);
		}

		[HttpPost]
		public async Task<IActionResult> Update(RecordUpdateDTO recordUpdateDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					string userName = User.Identity.Name;

					User user = await _userManager.FindByNameAsync(userName);

					Record existRecord = await _recordService.GetByIdAsync(recordUpdateDTO.Id);

					if (recordUpdateDTO.NewFiles != null)
					{
						List<UploadedFile> uploadedFileCreateDTOs = AddFiles(recordUpdateDTO.NewFiles, recordUpdateDTO.Id, userName);

						foreach (var picture in uploadedFileCreateDTOs)
						{
							await _uploadedFileService.CreateAsync(picture);
						}
					}

					if (recordUpdateDTO.UploadedFileViewDTOs != null)
					{
						foreach (var picture in recordUpdateDTO.UploadedFileViewDTOs)
						{
							if (picture.Delete == true)
							{
								System.IO.File.Delete(picture.Path);

								await _uploadedFileService.DeleteAsync(picture.Id);
							}
						}
					}	

					Record recordToUpdate = _mapper.Map<Record>(recordUpdateDTO);

					recordToUpdate.Text = _aesCryptoProvider.EncryptValue(recordUpdateDTO.DecryptedText, user.CryptoKey, existRecord.IvKey);
					await _recordService.UpdateEntryAsync(recordToUpdate);
				}
				catch (DbUpdateException ex)
				{
					return Content(ex.Message);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}

				return RedirectToAction("Index", "Home");
			}

			return View(recordUpdateDTO);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id != Guid.Empty)
			{
				Record record = await _recordService.GetByIdAsync(id);

				foreach(var file in record.UploadedFiles)
				{
					System.IO.File.Delete(file.Path);

					await _uploadedFileService.DeleteAsync(file.Id);
				}

				var deleteStatus = await _recordService.DeleteAsync(record.Id);

				if (deleteStatus)
					return RedirectToAction("Index", "Home");
			}

			return NotFound();
		}

		public List<UploadedFile> AddFiles(IFormFileCollection formFiles, Guid recordId, string userName)
		{
			List<UploadedFile> uploadedFiles = new List<UploadedFile>();

			foreach (var file in formFiles)
			{
				var pictureName = Guid.NewGuid() + "_" + file.FileName;

				// get path for check if Images folder exist
				string wwwrootImages = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

				if (!Directory.Exists(wwwrootImages))
				{
					Directory.CreateDirectory(wwwrootImages);
				}

				var path = Path.Combine(wwwrootImages, pictureName);

				using (Stream stream = file.OpenReadStream())
				{
					using (Image image = Image.Load(stream))
					{
						image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
						image.Save(path);
					}
				}

				var uploadedFileCreateDTO = new UploadedFileCreateDTO()
				{
					Name = pictureName,
					Path = path,
					RecordId = recordId,
					Created = DateTime.Now,
					CreatedBy = userName
				};

				var newPicture = _mapper.Map<UploadedFile>(uploadedFileCreateDTO);

				uploadedFiles.Add(newPicture);
			}

			return uploadedFiles;
		}
	}
}
