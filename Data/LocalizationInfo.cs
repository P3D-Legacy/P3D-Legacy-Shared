using System.Globalization;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.Data
{
    public struct LocalizationInfo
    {
        public CultureInfo CultureInfo { get; private set; }
        public string SubLanguage { get; private set; }

        [YamlIgnore]
        public string Author { get; private set; }

        public LocalizationInfo(CultureInfo cultureInfo, string subLanguage = "", string author = "") { CultureInfo = cultureInfo; SubLanguage = subLanguage; Author = author; }
    }
}
