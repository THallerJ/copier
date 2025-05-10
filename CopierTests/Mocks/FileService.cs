using Copier.Interfaces;

namespace CopierTests.Mocks
{
    class FileService : IFileService
    {
        private readonly FileSystem FileSystem;

        public FileService(FileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public void Copy(string src, string dest, bool overwrite = false)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            return true;
        }

        public FileAttributes GetAttributes(string path)
        {
            string[] paths = path.Split('/');

            if (!FileSystem.FileTree.TryGetValue(paths[0], out var files))
            {
                throw new FileNotFoundException();
            }

            var file = files.Find(x => x.Name == paths[1]);

            return file == null ? throw new FileNotFoundException() : file.Attribute;
        }

        public string ReadAllText(string path)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FileSystem.FileOutput);
        }

        public Task WriteAllTextAsync(string path, string content, CancellationToken cancellationToken = default)
        {
            FileSystem.FileOutput += content;
            return Task.CompletedTask;
        }
    }
}
