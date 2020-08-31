using System.IO;
using System.Security.Cryptography;
using Diary.Services.Interfaces;

namespace Diary.Services
{
	public class AesCryptoProviderService : IAesCryptoProviderService
	{
		public string DecryptValue(byte[] value, byte[] key, byte[] iv)
		{
			string dencryptedText;
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msDecrypt = new MemoryStream(value))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							dencryptedText = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return dencryptedText;
		}

		public byte[] EncryptValue(string value, byte[] key, byte[] iv)
		{
			byte[] encrypted;
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = iv;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(value);
						}

						encrypted = msEncrypt.ToArray();
					}
				}
			}

			return encrypted;
		}

		public byte[] GenerateIV()
		{
			byte[] cryptoKey;
			using (Aes aes = Aes.Create())
			{
				aes.GenerateIV();
				cryptoKey = aes.IV;
			}

			return cryptoKey;
		}

		public byte[] GenerateKey()
		{
			byte[] cryptoKey;
			using (Aes aes = Aes.Create())
			{
				aes.GenerateKey();
				cryptoKey = aes.Key;
			}

			return cryptoKey;
		}
	}
}
