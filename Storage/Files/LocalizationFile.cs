using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using CsvHelper;

using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public class LocalizationFile : ILocalizationFile
    {
        internal static CultureInfo DefaultLanguage { get; } = new CultureInfo("en");

        internal const string Prefix = "translation_";
        internal const string FileExtension = ".csv";


        public string Name => File.Name;
        public string Path => File.Path;

        public CultureInfo Language { get; }

        private IFile File { get; }
        private Dictionary<string, string> Tokens { get; } = new Dictionary<string, string>();

        public LocalizationFile(IFile file)
        {
            File = file;
            Language = new CultureInfo(Name.Replace(Prefix, "").Replace(FileExtension, ""));
            Reload();
        }
        public LocalizationFile(IFile file, CultureInfo language)
        {
            File = file;
            Language = language;
            Reload();
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

            return Tokens.TryGetValue(stringID, out var token) ? token : $"NOT FOUND: {stringID}";
        }

        public void Reload()
        {
            var customLang = !Equals(DefaultLanguage, Language);
            using (var parser = new CsvParser(new System.IO.StringReader(AsyncExtensions.RunSync(this.ReadAllTextAsync))))
            {
                string[] row;
                while ((row = parser.Read()) != null)
                {
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
        }
    }
}
