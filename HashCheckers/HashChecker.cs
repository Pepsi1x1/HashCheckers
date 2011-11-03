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
        RC2
    }

    public class HashChecker
    {
        public static string GetFileHash(string fullPathToFile, HashTypes hashType)
        {
            var bytes = File.ReadAllBytes(fullPathToFile);
            return GetHash(bytes, hashType);
        }

        public static string GetHash(string text, HashTypes hashType)
        {
            var encoder = new UTF8Encoding();
            var bytes = encoder.GetBytes(text);
            return GetHash(bytes, hashType);
        }

        public static string GetHash(byte[] bytes, HashTypes hashType)
        {
            dynamic cryptoService = GetCryptoServiceProvider(hashType);
            if (cryptoService == null) return null;

            var hashBytes = cryptoService.ComputeHash(bytes);
            return FormatHash(hashBytes);
        }

        private static string FormatHash(dynamic hashBytes)
        {
            string fileHash = null;
            foreach (var b in hashBytes)
            {
                var hashbyte = string.Format("{0:X2}", b);
                fileHash += hashbyte;
            }
            return fileHash;
        }

        private static dynamic GetCryptoServiceProvider(HashTypes hashType)
        {
            dynamic cryptoService;
            switch (hashType)
            {
                case HashTypes.MD5:
                    {
                        cryptoService = new MD5CryptoServiceProvider();
                    }
                    break;
                case HashTypes.SHA1:
                    {
                        cryptoService = new SHA1CryptoServiceProvider();
                    }
                    break;
                case HashTypes.SHA256:
                    {
                        cryptoService = new SHA256CryptoServiceProvider();
                    }
                    break;
                case HashTypes.SHA384:
                    {
                        cryptoService = new SHA256CryptoServiceProvider();
                    }
                    break;
                case HashTypes.SHA512:
                    {
                        cryptoService = new SHA512CryptoServiceProvider();
                    }
                    break;
                    case HashTypes.RC2:
                    {
                        cryptoService = new RC2CryptoServiceProvider();
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
