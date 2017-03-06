using P3D.Legacy.Shared.Data;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Files
{
    public interface ILocalizationFile : IFile
    {
        bool IsCustom { get; }
        LocalizationInfo LocalizationInfo { get; }

        string GetString(string stringID);
        void Reload();
    }
}
