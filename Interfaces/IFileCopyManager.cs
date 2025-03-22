using Copier.Models;

namespace Copier.Interfaces
{
    public interface IFileCopyManager
    {
        public CopyJob Job { get; }

        public List<IJob<CopyJobConfig>> CopyJobs { get; }

        public void RunCopyJob();

        public void RunCopyJob(string fromPath, string toPath);

        public Task<List<IJob<CopyJobConfig>>> SaveCopyJobAsync(string name);

        public Task GetCopyJobAsync(string name);

        public void SetToPath(string path);

        public void SetFromPath(string path);
    }
}
