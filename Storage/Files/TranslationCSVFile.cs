using System;
using System.Collections.Generic;
using System.IO;

using CsvHelper;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public sealed class TranslationCSVFile : BaseTranslationFile
    {
        private Dictionary<string, string> Tokens { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public TranslationCSVFile(IFile file) : base(file)
        {
            using (var parser = new CsvParser(new StringReader(this.ReadAllText())))
            {
                string[] row;
                while ((row = parser.Read()) != null)
                {
                    if (row.Length > 1 && !Tokens.ContainsKey(row[0]))
                        Tokens.Add(row[0], row[1] ?? string.Empty);
                }
            }

            Author = Tokens.ContainsKey("language_maintainer") ? Tokens["language_maintainer"] : string.Empty;
        }

        public override string GetString(string stringID)
        {
            if (stringID == null)
                stringID = string.Empty;

            string token;
            return Tokens.TryGetValue(stringID, out token) ? token : stringID;
        }
    }
}
