using System;
using System.Collections.Generic;

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

        private LocalizationFile LocalizationFile { get; set; }


        public Localization(LocalizationFolder localizationFolder, LocalizationInfo localizationInfo)
        {
            LoadTranslationFile(localizationFolder, localizationInfo);
        }
        private void LoadTranslationFile(LocalizationFolder localizationFolder, LocalizationInfo localizationInfo)
        {
            if (localizationFolder.CheckTranslationExists(localizationInfo))
                LocalizationFile = localizationFolder.GetTranslationFile(localizationInfo);
            else
            {
                if (localizationFolder.CheckTranslationExists(LocalizationFile.DefaultLocalizationInfo))
                    LocalizationFile = localizationFolder.GetTranslationFile(LocalizationFile.DefaultLocalizationInfo);
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
