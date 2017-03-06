using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using CsvHelper;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public class LocalizationFile : ILocalizationFile
    {
        internal static LocalizationInfo DefaultLocalizationInfo { get; } = new LocalizationInfo(new CultureInfo("en"));

        internal const string Prefix = "translation_";
        internal const string FileExtension = ".csv";


        public string Name => File.Name;
        public string Path => File.Path;

        public bool IsCustom => System.IO.Path.GetFileNameWithoutExtension(Name).EndsWith("_c");
        public LocalizationInfo LocalizationInfo { get; private set; }

        private IFile File { get; }
        private Dictionary<string, string> Tokens { get; } = new Dictionary<string, string>();

        public LocalizationFile(IFile file)
        {
            File = file;
            LocalizationInfo = new LocalizationInfo(GetCultureInfo(Name));
            Reload();
        }
        public LocalizationFile(IFile file, CultureInfo language)
        {
            File = file;
            LocalizationInfo = new LocalizationInfo(language);
            Reload();
        }
        private CultureInfo GetCultureInfo(string fileName)
        {
            var info = Regex.Replace(System.IO.Path.GetFileNameWithoutExtension(fileName), Prefix, "", RegexOptions.IgnoreCase);
            if (IsCustom)
            {
                info = info.Replace("_c", "");
                var customName = info.Split('_').Last();

                info = info.Remove(info.Length - 1 - customName.Length, customName.Length + 1); // -- '_' should be removed too
            }

            return new CultureInfo(info);
        }
        private void Reload()
        {
            var cultureInfo = LocalizationInfo.CultureInfo;
            var author = "";
            var customName = "";

            var customLang = IsCustom || !Equals(DefaultLocalizationInfo.CultureInfo, cultureInfo);
            using (var parser = new CsvParser(new System.IO.StringReader(AsyncExtensions.RunSync(this.ReadAllTextAsync))))
            {
                string[] row;
                while ((row = parser.Read()) != null)
                {
                    if (string.Equals(row[0], "LANGUAGE_NAME", StringComparison.OrdinalIgnoreCase))
                    {
                        customName = customLang ? row[3] : row[2];
                        continue;
                    }

                    if (string.Equals(row[0], "LANGUAGE_MAINTAINER", StringComparison.OrdinalIgnoreCase))
                    {
                        author = customLang ? row[3] : row[2];
                        continue;
                    }

                    // -- Token Section
                    if (string.Equals(row[0], "BEGIN", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] tokenRow;
                        while ((tokenRow = parser.Read()) != null && !string.Equals(tokenRow[0], "END", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!tokenRow[0].StartsWith(";") && !row[0].EndsWith(";"))
                                Tokens.Add(tokenRow[0], customLang ? tokenRow[3] : tokenRow[2]);
                        }
                    }
                    // -- Token Section

                }
            }

            LocalizationInfo = new LocalizationInfo(cultureInfo, customName, author);
        }

        public Task CopyAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => File.CopyAsync(newPath, collisionOption, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => File.DeleteAsync(cancellationToken);
        public Task MoveAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => File.MoveAsync(newPath, collisionOption, cancellationToken);
        public Task<System.IO.Stream> OpenAsync(FileAccess fileAccess, CancellationToken cancellationToken = default(CancellationToken)) => File.OpenAsync(fileAccess, cancellationToken);
        public Task RenameAsync(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists, CancellationToken cancellationToken = new CancellationToken()) => File.RenameAsync(newName, collisionOption, cancellationToken);

        public string GetString(string stringID)
        {
            if (stringID == null)
                stringID = string.Empty;

            string token;
            return Tokens.TryGetValue(stringID, out token) ? token : stringID;
        }
    }
}
