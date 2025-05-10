using Copier.Interfaces;

namespace CopierTests.Mocks
{
    class DirectoryService : IDirectoryService
    {
        private readonly FileSystem FileSystem;

        public DirectoryService(FileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public void CreateDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path, bool recursive = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            return true;
        }

        public string[] GetDirectories(string path)
        {
            if (path != "src" && path != "dest") return [];
           
            return FileSystem.FileTree[path].Select(x => path + "/" + x.Name).ToArray();
        }

        public string[] GetFiles(string path)
        {
            return [.. FileSystem.FileTree[path].Select(x => path + "/" + x.Name)];
        }
    }
}
