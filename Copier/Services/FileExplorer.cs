using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public class FileExplorer : IFileExplorer
    {
        private readonly IFileService FileService;
        private readonly IDirectoryService DirectoryService;

        public FileExplorer(IFileService fileService, IDirectoryService directoryService)
        {
            FileService = fileService;
            DirectoryService = directoryService;
        }

        public void FileMap(string folderPath, Action<string> process)
        {
            foreach (string filePath in DirectoryService.GetFiles(folderPath))
            {
                if (!IsSkippableFile(filePath))
                    process(filePath);
            }

            foreach (string filePath in DirectoryService.GetDirectories(folderPath))
            {
                if (!IsSkippableFile(filePath))
                    process(filePath);
            }
        }

        public bool IsSkippableFile(string filePath)
        {
            var attributes = FileService.GetAttributes(filePath);
            return attributes.HasFlag(FileAttributes.System) || attributes.HasFlag(FileAttributes.Hidden);
        }
    }
}
