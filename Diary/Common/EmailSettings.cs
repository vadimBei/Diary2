﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Common
{
	public static class EmailSettings
	{
		public const string smtpAdress = "smtp.gmail.com";

		public const int port = 465;

		public const bool cancellationToken = true;

		public const string userSender = "travelblog1.no.reply@gmail.com";

		public const string passwordSender = "TravelBlog111";

		public const string headerAdministrationSite = "Администрация сайта Diary";
	}
}
