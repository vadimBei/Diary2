using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Services.Interfaces
{
	public interface IRazorViewToStringRenderer
	{
		Task<string> RenderToStringAsync(string viewName, object model);
	}
}
