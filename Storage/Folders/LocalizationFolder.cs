using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public class LocalizationFolder : ILocalizationFolder
    {
        private IFolder Folder { get; }

        public string Name => Folder.Name;
        public string Path => Folder.Path;

        public LocalizationFolder(IFolder folder) { Folder = folder; }
        public LocalizationFolder(string path) { Folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => Folder.CheckExistsAsync(name, cancellationToken);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => Folder.CreateFileAsync(desiredName, option, cancellationToken);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => Folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => Folder.DeleteAsync(cancellationToken);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => Folder.GetFileAsync(name, cancellationToken);
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => Folder.GetFilesAsync(cancellationToken);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => Folder.GetFolderAsync(name, cancellationToken);
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => Folder.GetFoldersAsync(cancellationToken);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => Folder.MoveAsync(folder, collisionOption, cancellationToken);

        public async Task<ILocalizationFile> GetTranslationFileAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken))
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
        public async Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(LocalizationFile.Prefix) && file.Name.EndsWith(LocalizationFile.FileExtension)).Select(file => new LocalizationFile(file)).ToList<ILocalizationFile>();
    }
}
