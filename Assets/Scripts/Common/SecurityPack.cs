using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Assets.Scripts.Common
{
    /// <summary>
    /// Functions:
    /// - SecurityPack.Md5.Encode(string value) - calc Md5 hash
    /// - SecurityPack.Base64.Encode(string value) - Base4 encoding
    /// - SecurityPack.Base64.Decode(string value) - Base4 decoding
    /// - SecurityPack.B64X.Encrypt(string value, string key) - fast string encryption
    /// - SecurityPack.B64X.Decrypt(string value, string key) - fast string decryption
    /// - SecurityPack.AES.Encrypt(byte[] value, string password) - strong byte[] encryption
    /// - SecurityPack.AES.Encrypt(string value, string password) - strong string encryption
    /// - SecurityPack.AES.Decrypt(string value, string password) - strong string decryption
    /// - SecurityPack.CRandom.GetRandom() - get integer random
    /// - SecurityPack.CRandom.GetRandom(int maxValue) - get random integer with max value (not included)
    /// - SecurityPack.CRandom.GetRandom(int minValue, int maxValue) - get random integer with min value and max value (not included)
    /// - SecurityPack.CRandom.Chance(int chance) - chech random event with probability 0-100%
    /// - SecurityPack.CRandom.Chance(float chance) - chech random event with probability 0-1f
    /// - SecurityPack.GooglePlayStore.VerifyPurchase(string purchaseJson, string signatureBase64, string publicKeyXml) - verify purchase digital signature
    /// </summary>
    public static class SecurityPack
    {
        /// <summary>
        /// Md5 algoritm implementation
        /// </summary>
        public static class Md5
        {
            public static string Encode(string value)
            {
                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(value);
                var hash = md5.ComputeHash(inputBytes);
                var stringBuilder = new StringBuilder();

                foreach (var h in hash)
                {
                    stringBuilder.Append(h.ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Base64 algoritm implementation
        /// </summary>
        public static class Base64
        {
            public static string Encode(string value)
            {
                var bytes = Encoding.UTF8.GetBytes(value);

                return Convert.ToBase64String(bytes);
            }

            public static string Decode(string value)
            {
                var bytes = Convert.FromBase64String(value);

                return Encoding.UTF8.GetString(bytes);
            }
        }

        /// <summary>
        /// Fast encryption based on Base64 algoritm
        /// </summary>
        public class B64X
        {
            public static string Encrypt(string value, string key)
            {
                return Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(key)));
            }

            public static string Decrypt(string value, string key)
            {
                return Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), Encoding.UTF8.GetBytes(key)));
            }

            private static byte[] Encode(byte[] bytes, byte[] key)
            {
                var j = 0;

                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] ^= key[j];

                    if (++j == key.Length)
                    {
                        j = 0;
                    }
                }

                return bytes;
            }
        }

        /// <summary>
        /// AES (Advanced Encryption Standard) implementation with 128-bit key (default)
        /// - 128-bit AES is approved  by NIST, but not the 256-bit AES
        /// - 256-bit AES is slower than the 128-bit AES (by about 40%)
        /// - Use it for secure data protection
        /// - Do NOT use it for data protection in RAM (in most common scenarios)
        /// </summary>
        public static class AES
        {
            public static int KeyLength = 128;
            private const string SaltKey = "ShMG8hLyZ7k~Ge5@";
            private const string VIKey = "~6YUi0Sv5@|{aOZO";

            public static string Encrypt(byte[] value, string password)
            {
                var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes(VIKey));

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(value, 0, value.Length);
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                        memoryStream.Close();

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            public static string Encrypt(string value, string password)
            {
                return Encrypt(Encoding.UTF8.GetBytes(value), password);
            }

            public static string Decrypt(string value, string password)
            {
                var cipherTextBytes = Convert.FromBase64String(value);
                var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes(VIKey));

                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[cipherTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                        memoryStream.Close();
                        cryptoStream.Close();

                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                    }
                }
            }

            public static ProtectedValue Decrypt(ProtectedValue value, ProtectedValue password)
            {
                return new ProtectedValue(Decrypt(value.String, password.String));
            }
        }


        /// <summary>
        /// Real random based on cryptography
        /// </summary>
        public static class CRandom
        {
            private static readonly byte[] Buffer = new byte[1024];
            private static int _bufferOffset = Buffer.Length;
            private static readonly RNGCryptoServiceProvider CryptoProvider = new RNGCryptoServiceProvider();

            public static int GetRandom()
            {
                if (_bufferOffset >= Buffer.Length)
                {
                    FillBuffer();
                }

                var val = BitConverter.ToInt32(Buffer, _bufferOffset) & 0x7fffffff;

                _bufferOffset += sizeof(int);

                return val;
            }

            /// <summary>
            /// Get integer random. maxValue does not included
            /// </summary>
            public static int GetRandom(int maxValue)
            {
                return GetRandom() % maxValue;
            }

            /// <summary>
            /// Get integer random. maxValue does not included
            /// </summary>
            public static int GetRandom(int minValue, int maxValue)
            {
                if (maxValue < minValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                var range = maxValue - minValue;

                return minValue + GetRandom(range);
            }

            /// <summary>
            /// Chance 0-100
            /// </summary>
            public static bool Chance(int chance)
            {
                return GetRandom(0, 101) < chance;
            }

            /// <summary>
            /// Chance 0-1f
            /// </summary>
            public static bool Chance(float chance)
            {
                return Chance((int)(100 * chance));
            }

            private static void FillBuffer()
            {
                CryptoProvider.GetBytes(Buffer);
                _bufferOffset = 0;
            }
        }

        public static class GooglePlayStore
        {
            /// <summary>
            /// Verify Google Play purchase. Protect you app against hack via Freedom. More info: http://mrtn.me/blog/2012/11/15/checking-google-play-signatures-on-net/
            /// </summary>
            /// <param name="purchaseJson">Purchase JSON string</param>
            /// <param name="signatureBase64">Purchase signature string</param>
            /// <param name="publicKeyXml">XML public key. Use http://superdry.apphb.com/tools/online-rsa-key-converter to convert RSA public key from Developer Console</param>
            /// <returns></returns>
            public static bool VerifyPurchase(string purchaseJson, string signatureBase64, string publicKeyXml)
            {
                if (!publicKeyXml.StartsWith("<RSAKeyValue>"))
                {
                    throw new ArgumentException(PublicKeyXmlExeption);
                }

                using (var provider = new RSACryptoServiceProvider())
                {
                    try
                    {
                        provider.FromXmlString(publicKeyXml);

                        var signature = Convert.FromBase64String(signatureBase64);
                        var sha = new SHA1Managed();
                        var data = Encoding.UTF8.GetBytes(purchaseJson);

                        return provider.VerifyData(data, sha, signature);
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.Log(e);
                    }

                    return false;
                }
            }

            public static string GetPublicKeyXml(string publicKeyBase64)
            {
                throw new NotImplementedException(PublicKeyXmlExeption);
            }

            private const string PublicKeyXmlExeption =
                "Please visit http://superdry.apphb.com/tools/online-rsa-key-converter to generate public key XML";
        }
    }
}