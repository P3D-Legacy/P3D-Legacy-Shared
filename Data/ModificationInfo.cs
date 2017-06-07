using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.Data
{
    public enum ModificationCategories
    {
        GameMode,
        TexturePack,
        MusicPack,
        Maps,
        Other
    }
    public class ModificationInfo
    {
        private const char StarFilled = '★';
        private const char StarEmpty = '☆';

        private static string GetRating(int rating)
        {
            const int maxRating = 5;

            if(rating >= maxRating)
                return new string(StarFilled, maxRating);
            else
                return new string(StarFilled, rating) + new string(StarEmpty, maxRating - rating);
        }


        private static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder();
        private static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder();

        public static string Serialize(ModificationInfo modificationInfo)
        {
            var serializer = SerializerBuilder.Build();
            return serializer.Serialize(modificationInfo);
        }
        public static ModificationInfo Deserialize(string data)
        {
            var deserializer = DeserializerBuilder.Build();
            try { return deserializer.Deserialize<ModificationInfo>(data); }
            catch (YamlException) { return null; }
        }


        [YamlMember(Alias = "ID")]
        public string ID { get; set; }

        [YamlMember(Alias = "Author")]
        public string Author { get; set; }

        [YamlMember(Alias = "Name")]
        public string Name { get; set; }

        [YamlMember(Alias = "Description")]
        public string Description { get; set; }

        [YamlMember(Alias = "InGameDescription")]
        public string InGameDescription { get; set; }

        [YamlMember(Alias = "Category")]
        public ModificationCategories Category { get; set; }

        [YamlMember(Alias = "Version")]
        public System.Version Version { get; set; }

        [YamlMember(Alias = "GameVersion")]
        public System.Version GameVersion { get; set; }

        // -- Online Info

        [YamlIgnore]
        public long Downloads { get; set; }

        [YamlIgnore]
        public byte Rating { get; set; }

        [YamlIgnore]
        public string RatingString => GetRating(Rating);

        // -- Online Info
    }
}
