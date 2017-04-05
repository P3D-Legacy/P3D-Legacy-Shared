using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    /*
    public interface ILocalizationFolder : IFolder
    {
        Task<ILocalizationFile> GetTranslationFileAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckTranslationExistsAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
    */
    public class LocalizationFolder : BaseFolder
    {
        public LocalizationFolder(IFolder folder) : base(folder) { }
        public LocalizationFolder(string path) : base(path) { }

        public LocalizationFile GetTranslationFile(LocalizationInfo localizationInfo)
        {
            return GetTranslationFiles().Single(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (string.IsNullOrEmpty(localizationInfo.SubLanguage) != string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return false;

                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);

                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }
        public async Task<LocalizationFile> GetTranslationFileAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await GetTranslationFilesAsync(cancellationToken)).Single(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (string.IsNullOrEmpty(localizationInfo.SubLanguage) != string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return false;

                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);

                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }
        public bool CheckTranslationExists(LocalizationInfo localizationInfo)
        {
            return GetTranslationFiles().Any(file =>
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
            return (await GetTranslationFilesAsync(cancellationToken)).Any(file =>
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
        public IList<LocalizationFile> GetTranslationFiles() => GetFiles().Where(file => file.Name.StartsWith(LocalizationFile.Prefix) && file.Name.EndsWith(LocalizationFile.FileExtension)).Select(file => new LocalizationFile(file)).ToList();
        public async Task<IList<LocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(LocalizationFile.Prefix) && file.Name.EndsWith(LocalizationFile.FileExtension)).Select(file => new LocalizationFile(file)).ToList();
    }
}
