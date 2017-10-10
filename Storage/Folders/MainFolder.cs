using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public sealed class MainFolder : BaseFolder
    {
        public MainFolder() : base(new ApplicationFolder()) { }
    }
}
