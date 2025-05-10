using System.IO;

namespace Copier.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

        void CreateDirectory(string path);

        string[] GetFiles(string path);

        string[] GetDirectories(string path);

        bool Exists(string path);

        void Delete(string path, bool recursive = false);
    }
}
