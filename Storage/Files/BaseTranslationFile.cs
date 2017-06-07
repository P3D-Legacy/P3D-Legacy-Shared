using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public abstract class BaseTranslationFile : BaseFile
    {
        public string Author { get; protected set; }

        protected BaseTranslationFile(IFile file) : base(file) { }

        public abstract string GetString(string stringID);
    }
}
