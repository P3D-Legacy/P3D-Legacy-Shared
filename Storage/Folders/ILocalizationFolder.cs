using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public interface ILocalizationFolder : IFolder
    {
        Task<ILocalizationFile> GetTranslationFileAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckTranslationExistsAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
