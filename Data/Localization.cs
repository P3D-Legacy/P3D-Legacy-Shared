using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Extensions;
using P3D.Legacy.Shared.Storage.Files;
using P3D.Legacy.Shared.Storage.Folders;

namespace P3D.Legacy.Shared.Data
{
    public class Localization
    {
        // TODO: Fix this crappy workaround
        private Dictionary<string, Func<string>> Replacers { get; } = new Dictionary<string, Func<string>>()
        {
            {"\r\n", () => "\n"},
            {"\n", () => Environment.NewLine},
        };

        private ILocalizationFile LocalizationFile { get; set; }


        public Localization(ILocalizationFolder localizationFolder, LocalizationInfo localizationInfo)
        {
            AsyncExtensions.RunSync(() => LoadTranslationFile(localizationFolder, localizationInfo));
        }
        private async Task LoadTranslationFile(ILocalizationFolder localizationFolder, LocalizationInfo localizationInfo)
        {
            if (await localizationFolder.CheckTranslationExistsAsync(localizationInfo))
                LocalizationFile = await localizationFolder.GetTranslationFileAsync(localizationInfo);
            else
            {
                if (await localizationFolder.CheckTranslationExistsAsync(Storage.Files.LocalizationFile.DefaultLocalizationInfo))
                    LocalizationFile = await localizationFolder.GetTranslationFileAsync(Storage.Files.LocalizationFile.DefaultLocalizationInfo);
            }
        }

        public string GetString(string stringID)
        {
            var token = LocalizationFile.GetString(stringID);
            foreach (var replacer in Replacers)
                token = token.Replace(replacer.Key, replacer.Value?.Invoke());
            return token;
        }
    }
}
