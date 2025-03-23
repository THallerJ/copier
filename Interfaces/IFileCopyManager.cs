using Copier.Models;

namespace Copier.Interfaces
{
    public interface IFileCopyManager
    {
        public CopyJob Job { get; }

        public List<IJob<CopyJobConfig>> CopyJobs { get; }

        public void RunCopyJob();

        public void RunCopyJob(string SrcPath, string DestPath);

        public Task<List<IJob<CopyJobConfig>>> SaveCopyJobAsync(string name);

        public Task GetCopyJobAsync(string name);

        public void SetDestination(string path);

        public void SetSource(string path);
    }
}
