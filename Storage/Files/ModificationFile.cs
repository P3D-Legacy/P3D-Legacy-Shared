using P3D.Legacy.Shared.Data;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public class ModificationFile : BaseFile
    {
        public const string FileName = "ModificationInfo.yml";

        public ModificationInfo ModificationInfo { get; }

        public ModificationFile(IFile file) : base(file) { ModificationInfo = ModificationInfo.Deserialize(this.ReadAllText()); }
    }
}
