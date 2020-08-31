using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Services.Interfaces
{
	public interface IEmailService
	{
		Task SendAsync(string subject, string message, string email);
	}
}
