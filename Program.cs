using System;
using System.IO;
using System.Security.Cryptography;

namespace MD5Checker
{
	class Program
	{
		static void Main(string[] args)
		{
			var filepath = GetValue(args, 0);
			if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
			{
				PrintUsage();
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
				return;
			}

			var fileName = Path.GetFileName(filepath);

			var expectedHash = GetValue(args, 1);

			var cryptoService = new MD5CryptoServiceProvider();
			var fileHash = "";
			Console.WriteLine("Loading: {0}", fileName);
			Console.WriteLine();
			using (var fileStream = new FileStream(filepath, FileMode.Open))
			{
				Console.WriteLine("Computing hash...");
				var hashBytes = cryptoService.ComputeHash(fileStream);

				Console.WriteLine("{0} hash is:", fileName);
				foreach (var b in hashBytes)
				{
					var hashbyte = string.Format("{0:X2}", b);
					fileHash += hashbyte;
					Console.Write(hashbyte);
				}
			}
			Console.WriteLine();
			Console.WriteLine();

			if (string.IsNullOrEmpty(expectedHash))
			{
				Console.WriteLine("Press any key to exit");
				Console.ReadKey();
				return;
			}

			Console.WriteLine(expectedHash);
			Console.WriteLine("is provided hash.");
			Console.WriteLine();

			if(fileHash == expectedHash)
				Console.WriteLine("Success, file is valid");
			else
				Console.Write("Invalid, Hash does not match");

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		public static void PrintUsage()
		{
			Console.WriteLine(@"Usage:
MD5Checker.exe <filename> [<expected hash>]");
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
