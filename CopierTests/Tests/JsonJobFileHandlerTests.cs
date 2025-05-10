using Copier.Interfaces;
using Copier.Models;
using Copier.Services;
using CopierTests.Mocks;

namespace CopierTests.Tests
{
    public class JsonJobFileHandlerTests
    {
        [Fact]
        public async Task WriteAsyncTest()
        {
            var fileSystem = new FileSystem();
            var JsonJobFileHandler = CreateJsonJobFileHandler(fileSystem);

            IJob<CopyJobConfig> job1 = new CopyJob("1", "/a", "/b");
            await JsonJobFileHandler.WriteAsync("", job1);
            var content = "[{\"Id\":\"1\",\"Config\":{\"Src\":\"/a\",\"Dest\":\"/b\"},\"JobType\":\"CopyJob\"}]";
            Assert.Equal(content, fileSystem.FileOutput);
        }

        private IJsonJobFileHandler CreateJsonJobFileHandler(FileSystem fileSystem)
        {
            var fileService = new Mocks.FileService(fileSystem);
            var directoryService = new Mocks.DirectoryService(fileSystem);
            return new JsonJobFileHandler(fileService, directoryService);
        }
    }
}
