namespace Copier.Interfaces
{
    public interface IFileExplorer
    {
        bool IsSkippableFile(string filePath);

        void FileMap(string folderPath, Action<string> action);
    }
}
