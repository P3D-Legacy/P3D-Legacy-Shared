using System.ComponentModel;

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


        [YamlMember(Alias = "ID"), Browsable(false)]
        public string ID { get; set; }

        [YamlMember(Alias = "Author"), DisplayName("Author")]
        public string Author { get; set; }

        [YamlMember(Alias = "Name"), DisplayName("Name")]
        public string Name { get; set; }

        [YamlMember(Alias = "Description"), Browsable(false)]
        public string Description { get; set; }

        [YamlMember(Alias = "InGameDescription"), Browsable(false)]
        public string InGameDescription { get; set; }

        [YamlMember(Alias = "Category"), DisplayName("Category")]
        public ModificationCategories Category { get; set; }

        [YamlMember(Alias = "Version"), DisplayName("Version")]
        public System.Version Version { get; set; }

        [YamlMember(Alias = "GameVersion"), DisplayName("Game Version")]
        public System.Version GameVersion { get; set; }

        // -- Online Info

        [YamlIgnore, DisplayName("Downloads")]
        public long Downloads { get; set; }

        [YamlIgnore, Browsable(false)]
        public byte Rating { get; set; }

        [YamlIgnore, DisplayName("Rating")]
        public string RatingString => GetRating(Rating);

        // -- Online Info
    }
}
