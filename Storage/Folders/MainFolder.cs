using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public sealed class MainFolder : BaseFolder
    {
        public MainFolder() : base(FileSystem.SpecialStorage) { }
    }
}
