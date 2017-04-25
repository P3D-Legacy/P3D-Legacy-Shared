using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Storage.Folders
{
    public class TranslationFolder : BaseFolder
    {
        public LocalizationInfo LocalizationInfo { get; }

        public TranslationFolder(IFolder folder, LocalizationInfo localizationInfo) : base(folder) { LocalizationInfo = localizationInfo; }

        public BaseTranslationFile GetTranslationFile(string name) => GetTranslationFiles().FirstOrDefault(file => name.Equals(file.Name));
        public async Task<BaseTranslationFile> GetTranslationFileAsync(string name) => (await GetTranslationFilesAsync()).FirstOrDefault(file => name.Equals(file.Name));

        public IList<BaseTranslationFile> GetTranslationFiles()
        {
            var list = new List<BaseTranslationFile>();
            foreach (var file in GetFiles())
            {
                switch (PortablePath.GetExtension(file.Name))
                {
                    case ".csv":
                        list.Add(new TranslationCSVFile(file));
                        break;
                }      
            }
            return list;
        }
        public async Task<IList<BaseTranslationFile>> GetTranslationFilesAsync()
        {
            var list = new List<BaseTranslationFile>();
            foreach (var file in await GetFilesAsync())
            {
                switch (PortablePath.GetExtension(file.Name))
                {
                    case ".csv":
                        list.Add(new TranslationCSVFile(file));
                        break;
                }
            }
            return list;
        }
    }
}