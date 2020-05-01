using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Auth
{
	public class Password
	{
		public string PasswordHash { get; }
		public string Salt { get; }

		public Password(string password)
		{
			Salt = Guid.NewGuid().ToString().Substring(0, 8);

			PasswordHash = ComputePasswordHash(string.Concat(password, Salt));
		}

		public static bool CheckPassword(string password, string salt, string hash)
		{
			var passwordHash = ComputePasswordHash(string.Concat(password, salt));
			Console.WriteLine(passwordHash);
			return string.Equals(hash, passwordHash);
		}

		private static string ComputePasswordHash(string passwordWithSalt)
		{
			using SHA256 sha256 = SHA256.Create();

			byte[] inputBytes = Encoding.ASCII.GetBytes(passwordWithSalt);
			byte[] hashBytes = sha256.ComputeHash(inputBytes);

			StringBuilder stringBuilder = new StringBuilder();

			for (int i = 0; i < hashBytes.Length; i++)
			{
				stringBuilder.Append(hashBytes[i].ToString("X2"));
			}

			return stringBuilder.ToString();
		}
	}
}
