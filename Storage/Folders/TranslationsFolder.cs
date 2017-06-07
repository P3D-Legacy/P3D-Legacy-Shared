using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public class TranslationsFolder : BaseFolder
    {
        public TranslationsFolder(IFolder folder) : base(folder) { }
        
        public bool CheckTranslationExists(LocalizationInfo localizationInfo)
        {
            return GetTranslationFolders().Any(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);
                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }
        public async Task<bool> CheckTranslationExistsAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await GetTranslationFoldersAsync(cancellationToken)).Any(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);
                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }

        public TranslationFolder GetTranslationFolder(LocalizationInfo localizationInfo) => GetTranslationFolders().FirstOrDefault(folder => localizationInfo.Equals(folder.LocalizationInfo));
        public async Task<TranslationFolder> GetTranslationFolderAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFoldersAsync(cancellationToken)).FirstOrDefault(folder => localizationInfo.Equals(folder.LocalizationInfo));

        public IList<TranslationFolder> GetTranslationFolders()
        {
            var list = new List<TranslationFolder>();
            foreach (var folder in GetFolders())
            {
                CultureInfo cultureInfo;
                var cultureInfoName = folder.Name.Split('_').Length > 0 ? folder.Name.Split('_')[0] : string.Empty;
                var subLanguage = folder.Name.Split('_').Length > 1 ? folder.Name.Split('_')[1] : string.Empty;
                if (CultureInfoExtensions.TryGetCultureInfo(cultureInfoName, out cultureInfo))
                    list.Add(new TranslationFolder(folder, new LocalizationInfo(cultureInfo, subLanguage)));
            }
            return list;
        }
        public async Task<IList<TranslationFolder>> GetTranslationFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = new List<TranslationFolder>();
            foreach (var folder in await GetFoldersAsync(cancellationToken))
            {
                CultureInfo cultureInfo;
                var cultureInfoName = folder.Name.Split('_').Length > 0 ? folder.Name.Split('_')[0] : string.Empty;
                var subLanguage = folder.Name.Split('_').Length > 1 ? folder.Name.Split('_')[1] : string.Empty;
                if (CultureInfoExtensions.TryGetCultureInfo(cultureInfoName, out cultureInfo))
                    list.Add(new TranslationFolder(folder, new LocalizationInfo(cultureInfo, subLanguage)));
            }
            return list;
        }
    }
}