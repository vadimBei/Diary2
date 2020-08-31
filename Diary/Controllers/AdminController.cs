using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Entities.DTOs.Account;
using Diary.Entities.DTOs.Invite;
using Diary.Entities.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{

		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IInviteService _inviteService;
		private readonly IEmailService _emailService;

		public AdminController(IMapper mapper,
			IUserService userService,
			UserManager<User> userManager,
			RoleManager<AppRole> roleManager,
			IInviteService inviteService,
			IEmailService emailService)
		{
			_userManager = userManager;
			_userService = userService;
			_roleManager = roleManager;
			_mapper = mapper;
			_inviteService = inviteService;
			_emailService = emailService;
		}

		public async Task<IActionResult> Users()
		{

			var users = await _userService.GetAllUsersAsync();

			List<UserViewDTO> userViewDtos = new List<UserViewDTO>();

			foreach (var user in users)
			{
				// Gets a list of role names the specified user belongs to
				var rolesInUser = await _userManager.GetRolesAsync(user);

				var userViewDTO = _mapper.Map<UserViewDTO>(user);

				// User has only one role, so when we get roles list, we take zero [0] element from it.
				userViewDTO.RoleName = rolesInUser.FirstOrDefault();

				userViewDtos.Add(userViewDTO);
			}

			return View(userViewDtos);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			var deleteStatus = await _userService.DeleteAsync(id);

			if (deleteStatus)
				return RedirectToAction("Users", "Admin");

			return NotFound();
		}

		[HttpGet]
		public IActionResult GetEmailForInvitation()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetEmailForInvitation(InviteCreateDTO inviteCreateDTO)
		{
			var user = await _userManager.FindByEmailAsync(inviteCreateDTO.EmailNewUser);

			if (user != null)
			{
				return Content("Користувач з таким email вже існує");
			}

			var invite = _mapper.Map<Invite>(inviteCreateDTO);
			var newInvite = await _inviteService.CreateAsync(invite);

			var callBack = Url.Action(
				"CheckInvite",
				"Account",
				new { token = newInvite.Id },
				protocol: HttpContext.Request.Scheme);

			await _emailService.SendAsync("Запрошення для реєстрації на сайті",
				$"{inviteCreateDTO.Message} <a href='{callBack}'>Diary</a>", inviteCreateDTO.EmailNewUser);

			return RedirectToAction("Index", "Home");
		}
	}
}
