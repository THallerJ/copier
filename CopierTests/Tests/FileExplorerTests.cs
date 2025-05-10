using Copier.Interfaces;
using Copier.Services;
using CopierTests.Mocks;

namespace CopierTests.Tests
{
    public class FileExplorerTests: IClassFixture<FileSystem>
    {
        [Fact]
        public void FileMapTest()
        {
            var fileExplorer = CreateFileExplorer(new FileSystem());
            List<string> processedPaths = [];

            fileExplorer.FileMap("src", (path) =>
            {
                processedPaths.Add(path);
            });

            Assert.Contains("src/file1.txt", processedPaths);
            Assert.Contains("src/file3.txt", processedPaths);

            Assert.DoesNotContain("src/file2.txt", processedPaths);
            Assert.DoesNotContain("src/file4.txt", processedPaths);
            Assert.DoesNotContain("src/file5.txt", processedPaths);
        }

        [Fact]
        public void SkippableFileTest()
        {
            var fileExplorer = CreateFileExplorer(new FileSystem());

            Assert.False(fileExplorer.IsSkippableFile("src/file1.txt"));
            Assert.True(fileExplorer.IsSkippableFile("src/file2.txt"));
            Assert.True(fileExplorer.IsSkippableFile("src/file5.txt"));
        }

        private IFileExplorer CreateFileExplorer(FileSystem fileSystem)
        {
            var fileService = new Mocks.FileService(fileSystem);
            var directoryService = new Mocks.DirectoryService(fileSystem);
            return new FileExplorer(fileService, directoryService);
        }
    }
}
