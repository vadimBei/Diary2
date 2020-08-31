using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Entities.DTOs.Common
{
	public class CaptchaResult
	{
		public string CaptchaCode { get; set; }

		public byte[] CaptchaByteData { get; set; }

		public string CaptchBase64Data => Convert.ToBase64String(CaptchaByteData);

		public DateTime Timestamp { get; set; }
	}
}
