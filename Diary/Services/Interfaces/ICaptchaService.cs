using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace Diary.Services.Interfaces
{
	public interface ICaptchaService
	{
		string GenerateCaptchaCode();

		bool ValidateCaptchaCode(string userInputCaptcha, HttpContext context);

		CaptchaResult GenerateCaptchaImage(int width, int height, string captchaCode);
	}
}
