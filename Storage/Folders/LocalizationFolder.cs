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
    public class LocalizationFolder : IFolder
    {
        private readonly IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public LocalizationFolder(IFolder folder) { _folder = folder; }
        public LocalizationFolder(string path) { _folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public IFile GetFile(string name) => _folder.GetFile(name);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public IList<IFile> GetFiles() => _folder.GetFiles();
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public IFile CreateFile(string desiredName, CreationCollisionOption option) => _folder.CreateFile(desiredName, option);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public IFolder CreateFolder(string desiredName, CreationCollisionOption option) => _folder.CreateFolder(desiredName, option);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public IFolder GetFolder(string name) => _folder.GetFolder(name);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public IList<IFolder> GetFolders() => _folder.GetFolders();
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
        public ExistenceCheckResult CheckExists(string name) => _folder.CheckExists(name);
        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public void Delete() => _folder.Delete();
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public IFolder Move(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting) => _folder.Move(folder, option);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => _folder.MoveAsync(folder, option, cancellationToken);


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
