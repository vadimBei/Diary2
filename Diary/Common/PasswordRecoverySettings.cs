using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Common
{
	public class PasswordRecoverySettings
	{
		public const string subject = "Відновлення паролю";

		public static string GetMessageWihtFeedBack(string feedBack)
		{
			var message = $"Для відновлення паролю, перейдіть за посиланням <a href='{feedBack}'>Diary</a>";

			return message;
		}
	}
}
