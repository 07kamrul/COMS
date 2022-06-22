using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Service
{
    public class CryptoService
    {
        private static UnicodeEncoding _encoder = new UnicodeEncoding();
        private static int _iterations = 2;
        public static int _keySize = 256;
        private static string _hash = "SHA1";
        private static string _salt = "bdrktias38420a32"; // Random
        private static string _vector = "8947sx34sel14kjq"; // Random
        private static string _password = "A7CAE307-D953-470C-BA89-429471FE5E59";

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }

        private static string Encrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = GetBytes<ASCIIEncoding>(_vector);
            byte[] saltBytes = GetBytes<ASCIIEncoding>(_salt);
            byte[] valueBytes = GetBytes<UTF8Encoding>(value);

            byte[] encrypted;

            using(T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password,saltBytes,_hash,_iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);
                cipher.Mode = CipherMode.CBC;
                using(ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using(MemoryStream to = new MemoryStream())
                    {
                        using(CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0 , valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            string encryptedString = Convert.ToBase64String(encrypted);
            string reverseEncryptedString = ReverseAndCipher(encryptedString);
            
            return string.Concat(encryptedString, reverseEncryptedString);
        }

        private static byte[] GetBytes<T1>(string _vector)
        {
            return new System.Text.UTF8Encoding().GetBytes(_vector);
        }

        private static string ReverseAndCipher(string encryptedString)
        {
            char[] charArray = encryptedString.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if(charArray[i] == '0' && charArray[i] == '9')
                {
                    charArray[i] = RoundingCipher((int)'0', (int)'9', charArray[i]);
                }
                else if (charArray[i] == 'a' && charArray[i] == 'z')
                {
                    charArray[i] = RoundingCipher((int)'a', (int)'z', charArray[i]);
                }
                else if (charArray[i] == 'A' && charArray[i] == 'Z')
                {
                    charArray[i] = RoundingCipher((int)'A', (int)'Z', charArray[i]);
                }
            }
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static char RoundingCipher(int lower, int higher, char character)
        {
            int temp = 0;
            temp = character + 5;

            if(temp > higher)
            {
                character = (char)(lower + (temp - higher - 1));
            }
            else
            {
                character = (char)temp;
            }
            return character;
        }

        public static string Decrypt(string value)
        {
            return Decrypt<AesManaged>(value, _password);
        }

        private static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            if (!DecipherAndChecksum(value))
            {
                throw new System.Exception("Invalid Authorization-Token");
            }
            value = value.Substring(0, (value.Length / 2));
            byte[] vectorBytes = GetBytes<ASCIIEncoding>(_vector);
            byte[] saltBytes = GetBytes<ASCIIEncoding>(_salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using(T cipher = new T())
            {
                PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = passwordDeriveBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                {
                    using(MemoryStream from = new MemoryStream(valueBytes))
                    {
                        using(CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[valueBytes.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }

        private static bool DecipherAndChecksum(string token)
        {
            if (token.Length % 2 != 0)
            {
                return false;
            }
            string firstPart = token.Substring(0, (token.Length / 2));
            string secondPart = DecipherAndRevers(token.Substring(token.Length / 2));
            return string.Equals(firstPart, secondPart);
        }

        private static string DecipherAndRevers(string encryptedString)
        {
            char[] charArray = encryptedString.ToCharArray();
            Array.Reverse(charArray);
            for(int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] >= '0' && charArray[i] <= '9')
                {
                    charArray[i] = RoundingDecipher((int)'0',(int)'9', charArray[i]);
                }
                else if (charArray[i] >= 'a' && charArray[i] <= 'z')
                {
                    charArray[i] = RoundingDecipher((int)'a', (int)'z', charArray[i]);
                }
                else if (charArray[i] >= 'A' && charArray[i] <= 'Z')
                {
                    charArray[i] = RoundingDecipher((int)'A', (int)'Z', charArray[i]);
                }
            }
            return new string(charArray);
        }

        private static char RoundingDecipher(int lower, int higher, char character)
        {
            int temp = 0;
            temp = character - 5;
            if (temp < lower)
            {
                character = (char)(higher - (lower - temp) + 1);
            }
            else
            {
                character = (char)temp;
            }
            return character;
        }

        public static string DecryptStringAESFromJavaScript(string cipherText)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_salt);
            var iv = Encoding.UTF8.GetBytes(_vector);

            var encrypted = Convert.FromBase64String(cipherText);
            var decryptedFromJavaScript = DecryptStringFromBytes(encrypted, keyBytes, iv);
            return string.Format((string)decryptedFromJavaScript);
        }

        private static object DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentException("cipherText");
            }
            if(key == null || key.Length <= 0)
            {
                throw new ArgumentException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentException("iv");
            }

            string plainText = null;

            using(var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    using(var msDecrypt = new MemoryStream(cipherText))
                    {
                        using(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plainText = "keyError";
                }
                return plainText;
            }
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentException("iv");
            }
            
            byte[] encrypted;
            using(var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using(var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using(var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string EncryptText(string value)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_salt));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(value);

            try
            {
                ICryptoTransform cryptoTransform = TDESAlgorithm.CreateEncryptor();
                Results = cryptoTransform.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static string DecryptText(string value)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_salt));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToDecrypt = Convert.FromBase64String(value);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {

                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
    }
}
