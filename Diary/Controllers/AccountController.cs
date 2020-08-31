using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Common;
using Diary.Entities.DTOs.Account;
using Diary.Entities.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary.Controllers
{
	public class AccountController : Controller
	{
		private IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly ICaptchaService _captchaService;
		private readonly IInviteService _inviteService;
		private readonly IAesCryptoProviderService _aesCrypto;

		public AccountController(
			IUserService userService,
			IMapper mapper,
			IEmailService emailService,
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			RoleManager<AppRole> roleManager,
			ICaptchaService captchaService,
			IInviteService inviteService,
			IAesCryptoProviderService aesCrypto)
		{
			_userService = userService;
			_mapper = mapper;
			_aesCrypto = aesCrypto;
			_emailService = emailService;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_captchaService = captchaService;
			_inviteService = inviteService;
		}

		[HttpGet]
		public IActionResult Authenticate(string returnUrl = null)
		{
			return View(new UserAuthenticateDTO { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> Authenticate(UserAuthenticateDTO userAuthenticateDto)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(userAuthenticateDto.Email);

				if (user == null)
					return NotFound();

				var result =
					await _signInManager.PasswordSignInAsync(user, userAuthenticateDto.Password, userAuthenticateDto.RememberMe, false);

				if (result.Succeeded)
				{
					// проверяем, принадлежит ли URL приложению
					if (!string.IsNullOrEmpty(userAuthenticateDto.ReturnUrl) && Url.IsLocalUrl(userAuthenticateDto.ReturnUrl))
					{
						return Redirect(userAuthenticateDto.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
			return View(userAuthenticateDto);
		}

		// returns captcha image
		[Route("get-captcha-image")]
		public IActionResult GetCaptchaImage()
		{
			int width = 100;
			int height = 36;
			var captchaCode = _captchaService.GenerateCaptchaCode();
			var result = _captchaService.GenerateCaptchaImage(width, height, captchaCode);
			HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
			Stream s = new MemoryStream(result.CaptchaByteData);
			return new FileStreamResult(s, "image/png");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterDTO userRegisterDto)
		{
			if (ModelState.IsValid)
			{
				var checkedUser = await _userManager.FindByEmailAsync(userRegisterDto.Email);

				if (checkedUser != null)
					return Content("Користувач з таким email вже існує");

				byte[] cryptokey = _aesCrypto.GenerateKey();

				var user = _mapper.Map<User>(userRegisterDto);
				user.CryptoKey = cryptokey;

				// добавляем пользователя
				var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

				if (result.Succeeded)
				{
					var currentUser = await _userManager.FindByEmailAsync(user.Email);
					await _userManager.AddToRoleAsync(currentUser, "User");

					await _emailService.SendAsync(SeccessRegisterSettings.subject,
							SeccessRegisterSettings.CteateMessage(userRegisterDto), userRegisterDto.Email);

					// установка куки
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(userRegisterDto);
		}

		[HttpGet]
		public async Task<IActionResult> LogOff()
		{
			// delete authentication cookies
			await _signInManager.SignOutAsync();

			return RedirectToAction("Authenticate", "Account");
		}

		[HttpGet]
		public IActionResult PasswordRestoringLink()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> PasswordRestoringLink(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return BadRequest(new { message = $"User with {email} email wasn't find" });
			}

			var feedBack = Url.Action(
				"NewPassword",
				"Account",
				new { userEmail = email },
				protocol: HttpContext.Request.Scheme);

			await _emailService.SendAsync(PasswordRecoverySettings.subject,
				PasswordRecoverySettings.GetMessageWihtFeedBack(feedBack), email);

			return RedirectToAction("Authenticate", "Account");
		}

		[HttpGet]
		public async Task<IActionResult> NewPassword(string userEmail)
		{
			var existUser = await _userManager.FindByEmailAsync(userEmail);

			var userNewPasswordDto = _mapper.Map<UserNewPasswordDTO>(existUser);

			return View(userNewPasswordDto);
		}

		[HttpPost]
		public async Task<IActionResult> NewPassword(UserNewPasswordDTO userNewPasswordDto)
		{
			if (ModelState.IsValid && userNewPasswordDto.Id != Guid.Empty && userNewPasswordDto.NewPassword.Equals(userNewPasswordDto.NewPasswordConfirm))
			{
				var user = await _userManager.FindByIdAsync(userNewPasswordDto.Id.ToString());

				if (user != null)
				{
					var _passwordValidator = HttpContext
						.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;

					var _passwordHasher =
						HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

					IdentityResult result =
						await _passwordValidator.ValidateAsync(_userManager, user, userNewPasswordDto.NewPassword);

					if (result.Succeeded)
					{
						user.PasswordHash = _passwordHasher.HashPassword(user, userNewPasswordDto.NewPassword);

						await _userManager.UpdateAsync(user);

						await _emailService.SendAsync(ChangePasswordSettings.subject,
							ChangePasswordSettings.GetMessage(userNewPasswordDto.Email,
							userNewPasswordDto.NewPassword), userNewPasswordDto.Email);

						return RedirectToAction("Authenticate", "Account");
					}
					else
					{
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Користувач незнайдений");
				}
			}
			else
			{
				return Content("Model state isn't valid");
			}
			return View(userNewPasswordDto);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> ChangePassword(UserChangePasswordDTO userChangePasswordDto)
		{
			if (ModelState.IsValid)
			{
				var nameOfCurrentUser = HttpContext.User.Identity.Name;

				var user = await _userManager.FindByNameAsync(nameOfCurrentUser);

				if (user != null)
				{
					IdentityResult result =
						await _userManager.ChangePasswordAsync(user, userChangePasswordDto.OldPassword, userChangePasswordDto.NewPassword);

					if (result.Succeeded)
					{
						await _emailService.SendAsync(ChangePasswordSettings.subject,
							ChangePasswordSettings.GetMessage(user.Email, userChangePasswordDto.NewPassword),
							user.Email);

						return RedirectToAction("Index", "Home");
					}
					else
					{
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Користувач незнайдений");
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Неправильно введені дані");
			}
			return PartialView(userChangePasswordDto);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			User user = new User();

			if (id == Guid.Empty)
			{
				var userName = HttpContext.User.Identity.Name;
				user = await _userManager.FindByNameAsync(userName);
			}
			else
			{
				user = await _userManager.FindByIdAsync(id.ToString());
			}

			if (user == null)
				return NotFound();

			var userToView = _mapper.Map<UserUpdateDTO>(user);

			var roles = await _roleManager.Roles.ToListAsync();

			var rolesInUser = await _userManager.GetRolesAsync(user);

			userToView.RolesInCurrentUser = rolesInUser.ToList();
			userToView.AllRoles = roles;

			return View(userToView);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UserUpdateDTO userUpdateDto, List<string> roles)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());


					if (user != null)
					{
						user.UserName = userUpdateDto.UserName;
						user.Email = userUpdateDto.Email;
					}

					if (User.IsInRole("Admin"))
					{
						// получем список ролей пользователя
						var userRoles = await _userManager.GetRolesAsync(user);
						// получаем все роли
						var allRoles = _roleManager.Roles.ToList();
						// получаем список ролей, которые были добавлены
						var addedRoles = roles.Except(userRoles);
						// получаем роли, которые были удалены
						var removedRoles = userRoles.Except(roles);

						await _userManager.AddToRolesAsync(user, addedRoles);
						await _userManager.RemoveFromRolesAsync(user, removedRoles);
					}

					await _userManager.UpdateAsync(user);
				}
				catch (DbUpdateException ex)
				{
					return Content(ex.Message);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}

				if (User.IsInRole("Admin"))
					return RedirectToAction("Users", "Admin");

				return RedirectToAction("Index", "Home");
			}

			return View(userUpdateDto);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> CheckInviteAsync(Guid token)
		{
			var invite = await _inviteService.GetByIdAsync(token);

			if (invite != null)
			{
				if (invite.Disabled == false)
				{
					invite.Disabled = true;

					await _inviteService.UpdateAsync(invite);

					return RedirectToAction("Register", "Account");
				}
				else
					return Content("Недійсне запрошення!");
			}

			return NotFound();
		}
	}
}
