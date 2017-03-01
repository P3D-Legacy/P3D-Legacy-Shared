using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<ILocalizationFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Single(file => Equals(file.Language, language));
        public async Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(LocalizationFile.Prefix) && file.Name.EndsWith(LocalizationFile.FileExtension)).Select(file => new LocalizationFile(file)).ToList<ILocalizationFile>();
        public async Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Any(file => Equals(file.Language, language));
    }
}
