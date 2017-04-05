using System.IO;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public abstract class BaseFile : IFile
    {
        private readonly IFile _file;
        public string Name => _file.Name;
        public string Path => _file.Path;
        public string LocalPath => Path.Remove(0, FileSystem.Current.BaseStorage.Path.Length).TrimStart('/', '|');

        public BaseFile(IFile file) { _file = file; }
        public BaseFile(string path) : this(FileSystem.Current.GetFileFromPath(path)) { }

        public Stream Open(PCLExt.FileStorage.FileAccess fileAccess) => _file.Open(fileAccess);
        public Task<Stream> OpenAsync(PCLExt.FileStorage.FileAccess fileAccess, CancellationToken cancellationToken = default(CancellationToken)) => _file.OpenAsync(fileAccess, cancellationToken);
        public void Delete() => _file.Delete();
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _file.DeleteAsync(cancellationToken);
        public void Rename(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists) => _file.Rename(newName, collisionOption);
        public Task RenameAsync(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken)) => _file.RenameAsync(newName, collisionOption, cancellationToken);
        public void Move(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting) => _file.Move(newPath, collisionOption);
        public Task MoveAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.MoveAsync(newPath, collisionOption, cancellationToken);
        public void Copy(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting) => _file.Copy(newPath, collisionOption);
        public Task CopyAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.CopyAsync(newPath, collisionOption, cancellationToken);
    }
}
