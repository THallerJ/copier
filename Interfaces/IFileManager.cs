namespace Copier.Interfaces
{
    public interface IFileManager
    {
        bool IsSkippableFile(string filePath);

        void FileMap(string folderPath, Action<string> action);

        void CopyDirectory(string from, string to);
    }
}
