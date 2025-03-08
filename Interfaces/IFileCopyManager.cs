using Copier.Models;

namespace Copier.Interfaces
{
    public interface IFileCopyManager
    {
        public CopyJob Job { get; }

        public void RunCopyJob();

        public void RunCopyJob(string fromPath, string toPath);

        public Task SaveCopyJobAsync(string name);

        public Task GetCopyJobAsync(string name);

        public void SetToPath(string path);

        public void SetFromPath(string path);
    }
}
