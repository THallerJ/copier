using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public class FileService : IFileService
    {
        public void Copy(string src, string dest, bool overwrite = false)
        {
            File.Copy(src, dest, overwrite);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
        {
            return File.ReadAllTextAsync(path, cancellationToken);
        }

        public Task WriteAllTextAsync(string path, string content, CancellationToken cancellationToken = default)
        {
            return File.WriteAllTextAsync(path, content, cancellationToken);
        }
    }
}
