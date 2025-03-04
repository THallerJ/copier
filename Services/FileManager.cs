using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public class FileManager : IFileManager
    {
        public void FileMap(string folderPath, Action<string> process)
        {
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                if (!IsSkippableFile(filePath))
                    process(filePath);
            }

            foreach (string filePath in Directory.GetDirectories(folderPath))
            {
                if (!IsSkippableFile(filePath))
                    process(filePath);
            }
        }
        public bool IsSkippableFile(string filePath)
        {
            var attributes = File.GetAttributes(filePath);
            return attributes.HasFlag(FileAttributes.System) || attributes.HasFlag(FileAttributes.Hidden);
        }
    }
}
