using System.Globalization;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public interface ILocalizationFile : IFile
    {
        CultureInfo Language { get; }

        string GetString(string stringID);
        void Reload();
    }
}
