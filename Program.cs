using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MD5Checker
{
	class Program
	{
		static void Main(string[] args)
		{
			var filename = GetValue(args, 0);
			if(string.IsNullOrEmpty(filename) || !File.Exists(filename))
				throw new FileNotFoundException("Specify a file to check");

			var expectedHash = GetValue(args, 1);

			var cryptoService = new MD5CryptoServiceProvider();

			var fileStream = new FileStream(filename, FileMode.Open);
			var hash = cryptoService.ComputeHash(fileStream);
			var fileHash = "";
			Console.WriteLine("{0} hash is:", filename);
			foreach (var b in hash)
			{
				var hashbyte = string.Format("{0:X2}", b);
				fileHash += hashbyte;
				Console.Write(hashbyte);
			}
			Console.WriteLine();

			if (string.IsNullOrEmpty(expectedHash))
			{
				Console.ReadKey();
				return;
			}

			Console.WriteLine();
			Console.WriteLine("is provided hash.");
			Console.WriteLine();

			if(fileHash == expectedHash)
				Console.WriteLine("Success, file is valid");
			else
				Console.Write("Invalid, Hash does not match");

			Console.ReadKey();
		}

		public static string GetValue(string[] arg, int index, string defaultValue)
		{
			return (arg == null || index >= arg.Length || string.IsNullOrEmpty(arg[index])) ? defaultValue : arg[index];
		}

		public static string GetValue(string[] arg, int index)
		{
			return (arg == null || index >= arg.Length) ? null : arg[index];
		}
	}
}
