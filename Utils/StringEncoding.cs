using System;
using System.Text;

namespace P3D.Legacy.Shared.Utils
{
    public class StringEncoding
    {
        public StringEncoding() { }

        public string Encode(string data) => Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        public string Decode(string data)
        {
            var arr = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(arr, 0, arr.Length);
        }

        public string EncodeRaw(byte[] data) => Convert.ToBase64String(data);
        public byte[] DecodeRaw(string data) => Convert.FromBase64String(data);
    }
}