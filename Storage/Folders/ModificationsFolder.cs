using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public class ModificationsFolder : BaseFolder
    {
        public ModificationsFolder(IFolder folder) : base(folder) { }

        public IList<ModificationFolder> GetModificationFolders() 
            => GetFolders().Where(folder => folder.CheckExists(ModificationFile.FileName) == ExistenceCheckResult.FileExists).Select(folder => new ModificationFolder(folder)).ToList();
        public async Task<IList<ModificationFolder>> GetModificationFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (await GetFoldersAsync(cancellationToken)).Where(folder => folder.CheckExists(ModificationFile.FileName) == ExistenceCheckResult.FileExists).Select(folder => new ModificationFolder(folder)).ToList();
    }
}
