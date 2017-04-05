using System.Collections.Generic;

using PCLExt.FileStorage;

namespace P3D.Legacy.Shared.Extensions
{
    public static class IFolderExtensions
    {
        public static IList<IFile> GetAllFiles(this IFolder folder, string path = "", bool firstFolder = true)
        {
            var files = new List<IFile>();

            foreach (var inFolder in folder.GetFolders())
            {
                var thisFolderName = firstFolder ? "" : folder.Name;
                files.AddRange(inFolder.GetAllFiles(string.IsNullOrEmpty(path) ? thisFolderName : $"{path}|{thisFolderName}"));
            }

            files.AddRange(folder.GetFiles());

            return files;
        }
    }
}
