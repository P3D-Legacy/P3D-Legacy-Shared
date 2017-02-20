using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace P3D.Legacy.Shared
{
    public class StringEncryption
    {
        private Random Random { get; } = new Random();
        private byte[] Key { get; }
        private RijndaelManaged RM { get; } = new RijndaelManaged();
        private UTF8Encoding Encoder { get; } = new UTF8Encoding();

        public StringEncryption(byte[] key) { Key = key; }

        public string Encrypt(string data)
        {
            var vector = new byte[16];
            Random.NextBytes(vector);
            var cryptogram = vector.Concat(Encrypt(Encoder.GetBytes(data), vector));
            return Convert.ToBase64String(cryptogram.ToArray());
        }
        public string Decrypt(string data)
        {
            var cryptogram = Convert.FromBase64String(data);
            if (cryptogram.Length < 17)
                throw new ArgumentException("Not a valid encrypted string", nameof(data));

            var vector = cryptogram.Take(16).ToArray();
            var buffer = cryptogram.Skip(16).ToArray();
            return Encoder.GetString(Decrypt(buffer, vector));
        }

        private byte[] Encrypt(byte[] buffer, byte[] vector) => Transform(buffer, RM.CreateEncryptor(Key, vector));
        private byte[] Decrypt(byte[] buffer, byte[] vector) => Transform(buffer, RM.CreateDecryptor(Key, vector));

        private static byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                cs.Write(buffer, 0, buffer.Length);
            return stream.ToArray();
        }
    }
}