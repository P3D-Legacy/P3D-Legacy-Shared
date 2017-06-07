using System.Text;

namespace P3D.Legacy.Shared.Utils
{
    public class StringEncryption
    {
        private string Key { get; }
        private UTF8Encoding Encoder { get; } = new UTF8Encoding();

        public StringEncryption(byte[] key) { Key = Encoder.GetString(key, 0, key.Length); }

        public string Encrypt(string data) => EncryptDecrypt(data);
        public string Decrypt(string data) => EncryptDecrypt(data);

        private string EncryptDecrypt(string input)
        {
            var output = new char[input.Length];
            for (var i = 0; i < input.Length; i++)
                output[i] = (char) (input[i] ^ Key[i % Key.Length]);
            return new string(output);
        }
    }
}