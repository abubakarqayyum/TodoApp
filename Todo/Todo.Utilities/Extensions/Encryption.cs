using CommonService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Utilities.Encryption
{
    public static class Encryption
    {
        public static readonly string _encryptionKey = "Zx9eR3rA6pD1vLwT7qHnJ4mY2cK8tV0b";
        public static readonly string _encryptionIV = "A1B2C3D4E5F6G7H8";

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(_encryptionIV);
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            string base64 = Convert.ToBase64String(encryptedBytes);


            string urlSafeBase64 = base64
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "~");
            return urlSafeBase64;
        }
        public static string Decrypt(string encryptedText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(_encryptionIV);
            aes.Padding = PaddingMode.PKCS7;
            using var decryptor = aes.CreateDecryptor();
            string base64 = encryptedText
                .Replace("-", "+")
                .Replace("_", "/")
                .Replace("~", "=");

            byte[] encryptedBytes = Convert.FromBase64String(base64);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
