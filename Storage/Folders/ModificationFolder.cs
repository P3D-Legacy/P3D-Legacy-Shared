using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public class ModificationFolder : BaseFolder
    {
        public ModificationFile ModificationFile => CheckExists(ModificationFile.FileName) == ExistenceCheckResult.FileExists ? new ModificationFile(GetFile("")) : null;

        public ModificationFolder(IFolder folder) : base(folder) { } 
    }
}