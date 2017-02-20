using System;
using System.Text;

namespace P3D.Legacy.Shared
{
    public class StringEncoding
    {
        public StringEncoding() { }

        public string Encrypt(string data) => Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        public string Decrypt(string data) => Encoding.UTF8.GetString(Convert.FromBase64String(data));

        public string EncryptRaw(byte[] data) => Convert.ToBase64String(data);
        public byte[] DecryptRaw(string data) => Convert.FromBase64String(data);
    }
}