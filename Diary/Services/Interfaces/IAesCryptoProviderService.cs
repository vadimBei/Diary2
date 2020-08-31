using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Services.Interfaces
{
	public interface IAesCryptoProviderService
	{
		byte[] GenerateKey();

		byte[] GenerateIV();

		byte[] EncryptValue(string value, byte[] key, byte[] iv);

		string DecryptValue(byte[] value, byte[] key, byte[] iv);
	}	
}
