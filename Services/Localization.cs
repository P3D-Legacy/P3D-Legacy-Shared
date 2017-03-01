using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Extensions;
using P3D.Legacy.Shared.Storage.Files;
using P3D.Legacy.Shared.Storage.Folders;

namespace P3D.Legacy.Shared.Services
{
    public class Localization
    {
        private Dictionary<string, Func<string>> Replacers { get; } = new Dictionary<string, Func<string>>();

        private ILocalizationFile LocalizationFile { get; set; }


        public Localization(ILocalizationFolder localizationFolder, CultureInfo language)
        {
            AsyncExtensions.RunSync(() => LoadTranslationFile(localizationFolder, language));
            //LoadTranslationFile(localizationFolder, language).RunSync();
        }
        private async Task LoadTranslationFile(ILocalizationFolder localizationFolder, CultureInfo language)
        {
            if (await localizationFolder.CheckTranslationExistsAsync(language))
                LocalizationFile = await localizationFolder.GetTranslationFileAsync(language);
            else
            {
                if (await localizationFolder.CheckTranslationExistsAsync(Storage.Files.LocalizationFile.DefaultLanguage))
                    LocalizationFile = await localizationFolder.GetTranslationFileAsync(Storage.Files.LocalizationFile.DefaultLanguage);
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
