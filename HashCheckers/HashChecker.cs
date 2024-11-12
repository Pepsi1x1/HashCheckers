using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HashCheckers
{
    public enum HashTypes
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512,
    }

    public class HashChecker
    {
        public static string GetFileHash(string fullPathToFile, HashTypes hashType)
        {
            using var stream = File.OpenRead(fullPathToFile);
            return GetHash(stream, hashType);
        }

        public static string GetHash(string text, HashTypes hashType)
        {
            var encoder = new UTF8Encoding();
            var bytes = encoder.GetBytes(text);
            return GetHash(bytes, hashType);
        }

        public static string GetHash(byte[] bytes, HashTypes hashType)
        {
            HashAlgorithm cryptoService = GetCryptoServiceProvider(hashType);
            if (cryptoService == null) return null;

            var hashBytes = cryptoService.ComputeHash(bytes);
            return FormatHash(hashBytes);
        }

        public static string GetHash(FileStream stream, HashTypes hashType)
        {
            HashAlgorithm cryptoService = GetCryptoServiceProvider(hashType);
            if (cryptoService == null) return null;

            var hashBytes = cryptoService.ComputeHash(stream);

            return FormatHash(hashBytes);
        }

        private static string FormatHash(byte[] hashBytes)
        {
            string fileHash = null;
            foreach (var b in hashBytes)
            {
                var hashbyte = string.Format("{0:X2}", b);
                fileHash += hashbyte;
            }
            return fileHash;
        }

        private static HashAlgorithm GetCryptoServiceProvider(HashTypes hashType)
        {
            HashAlgorithm cryptoService;
            switch (hashType)
            {
                case HashTypes.MD5:
                    {
                        cryptoService = MD5.Create();
                    }
                    break;
                case HashTypes.SHA1:
                    {
                        cryptoService = SHA1.Create();
                    }
                    break;
                case HashTypes.SHA256:
                    {
                        cryptoService = SHA256.Create();
                    }
                    break;
                case HashTypes.SHA384:
                    {
                        cryptoService = SHA384.Create();
                    }
                    break;
                case HashTypes.SHA512:
                    {
                        cryptoService = SHA512.Create();
                    }
                    break;
                default:
                    {
                        return null;
                    }
            }
            return cryptoService;
        }
    }
}
