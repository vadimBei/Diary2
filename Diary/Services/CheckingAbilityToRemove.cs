using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.DTOs.Record;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class CheckingAbilityToRemove : ICheckingAbilityToRemove
	{
		public bool Check(DateTime created)
		{
			// Checking if a record can be deleted
			DateTime today = DateTime.Now;

			System.TimeSpan timeSpan = today.Subtract(created);

			var days = timeSpan.Days;

			if (days > 2)
				return false;
			else
				return true;
		}
	}
}
