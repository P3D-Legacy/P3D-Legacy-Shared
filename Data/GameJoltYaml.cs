using P3D.Legacy.Shared.YamlConverters;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.Data
{
    public class GameJoltYaml
    {
        private static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new EncodedStringConverter());
        private static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new EncodedStringConverter());

        public static string Serialize(GameJoltYaml gameJoltYaml)
        {
            var serializer = SerializerBuilder.Build();
            return serializer.Serialize(gameJoltYaml);
        }
        public static GameJoltYaml Deserialize(string data)
        {
            var deserializer = DeserializerBuilder.Build();
            try { return deserializer.Deserialize<GameJoltYaml>(data); }
            catch (YamlException) { return null; }
        }

        [YamlMember(Alias = "GameId")]
        public EncodedString GameId { get; set; }
        [YamlMember(Alias = "GameKey")]
        public EncodedString GameKey { get; set; }

        [YamlMember(Alias = "Username")]
        public EncodedString Username { get; set; }
        [YamlMember(Alias = "Token")]
        public EncodedString Token { get; set; }
    }
}
