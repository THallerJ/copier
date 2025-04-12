using Copier.Models;

namespace Copier.Interfaces
{
    public interface IFileCopyManager
    {
        public CopyJob Job { get; set; }

        public List<IJob<CopyJobConfig>> CopyJobs { get; }

        public void RunCopyJob(IProgress<float> progress, CancellationToken cancellationToken);

        public void RunCopyJob(string SrcPath, string DestPath, IProgress<float> progress, CancellationToken cancellationToken);

        public Task<List<IJob<CopyJobConfig>>> SaveCopyJobAsync(string name);

        public Task<List<IJob<CopyJobConfig>>> DeleteJobAsync(string id);

        public void SetDestination(string path);

        public void SetSource(string path);

        public void Clear();
    }
}
