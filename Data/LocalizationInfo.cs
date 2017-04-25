using System.Globalization;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.Data
{
    public struct LocalizationInfo
    {
        [YamlMember(Alias = "CultureInfo")]
        public CultureInfo CultureInfo { get; private set; }
        [YamlMember(Alias = "SubLanguage")]
        public string SubLanguage { get; private set; }

        public LocalizationInfo(CultureInfo cultureInfo, string subLanguage = "") { CultureInfo = cultureInfo; SubLanguage = subLanguage; }
    }
}
