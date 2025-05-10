using System.IO;

namespace Copier.Interfaces
{
    public interface IFileService
    {
        void Copy(string src, string dest, bool overwrite = false);

        FileAttributes GetAttributes(string path);

        bool Exists(string path);

        string ReadAllText(string path);

        Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);

        Task WriteAllTextAsync(string path, string content, CancellationToken cancellationToken = default);
    }
}
