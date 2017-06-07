using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Utils
{
    public class Localization
    {
        // TODO: Fix this crappy workaround
        private Dictionary<string, Func<string>> Replacers { get; } = new Dictionary<string, Func<string>>()
        {
            {"\r\n", () => "\n"},
            {"\n", () => Environment.NewLine},
        };

        private TranslationsFolder TranslationsFolder { get; }
        private List<BaseTranslationFile> LocalizationFiles { get; set; }

        public LocalizationInfo[] Localizations => TranslationsFolder.GetTranslationFolders().Select(tf => tf.LocalizationInfo).ToArray();
        //public string[] Authors => LocalizationFiles.Select(lf => lf.Author).ToArray();
        public string[] AllAuthors => TranslationsFolder.GetTranslationFolders().SelectMany(tFolder => tFolder.GetTranslationFiles()).Select(lf => lf.Author).ToArray();


        public Localization(LocalizationInfo localizationInfo, IFolder translationsFolder = null)
        {
            if (translationsFolder == null)
                TranslationsFolder = new TranslationsFolder(new MainFolder().CreateFolder("Translation", CreationCollisionOption.OpenIfExists));
            else
                TranslationsFolder = new TranslationsFolder(translationsFolder);

            LoadTranslationFiles(TranslationsFolder.GetTranslationFolder(localizationInfo));
        }
        private void LoadTranslationFiles(TranslationFolder translationFolder)
        {
            LocalizationFiles = translationFolder.GetTranslationFiles().ToList();
        }

        public string GetString(string stringID)
        {
            foreach (var localizationFile in LocalizationFiles)
            {
                var token = localizationFile.GetString(stringID);
                foreach (var replacer in Replacers)
                    token = token.Replace(replacer.Key, replacer.Value?.Invoke());
                return token;
            }
            return stringID;
        }
    }
}
