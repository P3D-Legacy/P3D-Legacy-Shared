using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public abstract class BaseTranslationFile : BaseFile
    {
        public string Author { get; protected set; }

        public BaseTranslationFile(IFile file) : base(file) { }

        public abstract string GetString(string stringID);
    }
}
