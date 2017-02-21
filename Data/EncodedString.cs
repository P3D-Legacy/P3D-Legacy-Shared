namespace P3D.Legacy.Shared.Data
{
    public struct EncodedString
    {
        public static implicit operator EncodedString(string str) => new EncodedString(str);
        public static implicit operator string(EncodedString encodedString) => encodedString.Data;

        public static EncodedString FromEncodedData(string encryptedData) => new EncodedString() { EncodedData = encryptedData, Encoder = new StringEncoding() };
        public static string GetEncryptedData(EncodedString encodedString) => encodedString.EncodedData;


        private string EncodedData { get; set; }
        private StringEncoding Encoder { get; set; }
        private string Data { get { return Encoder.Decode(EncodedData); } set { EncodedData = Encoder.Encode(value); } }

        public EncodedString(string str) { EncodedData = string.Empty; Encoder = new StringEncoding(); Data = str; }

        public override string ToString() => Data;
    }
}
