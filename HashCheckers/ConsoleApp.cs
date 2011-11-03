using System;
using System.IO;

namespace HashCheckers
{
    public enum ErrorCodes
    {
        InvalidUsage = -1,
        Success = 0,
        InvalidHash = 1
    }
    public static class ConsoleApp
    {
        public static ErrorCodes Run(string[] args, HashTypes hashType)
        {
            var filepath = ExeUtil.GetValue(args, 0);
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                return ErrorCodes.InvalidUsage;
            }

            var expectedHash = ExeUtil.GetValue(args, 1);

            var fileName = Path.GetFileName(filepath);
            Console.WriteLine("Getting {0} hash for {1}...", hashType, fileName);
            var fileHash = HashChecker.GetFileHash(filepath, hashType);
            Console.WriteLine(fileHash);

            Console.WriteLine();


            if (string.IsNullOrWhiteSpace(expectedHash))
            {
                return ErrorCodes.Success;
            }
            expectedHash = expectedHash.Trim();

            Console.WriteLine(expectedHash);
            Console.WriteLine("is provided hash.");
            Console.WriteLine();

            if (string.Equals(fileHash, expectedHash, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Success, file is valid");
            }
            else
            {
                Console.Write("Invalid, Hash does not match");
                return ErrorCodes.InvalidHash;
            }

            return ErrorCodes.Success;
        }
    }
}
