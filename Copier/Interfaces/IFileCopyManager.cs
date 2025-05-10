using Copier.Models;

namespace Copier.Interfaces
{
    public interface IFileCopyManager
    {
        CopyJob Job { get; set; }

        List<IJob<CopyJobConfig>> CopyJobs { get; }

        void RunCopyJob(IProgress<float> progress, CancellationToken cancellationToken);

        void RunCopyJob(string SrcPath, string DestPath, IProgress<float> progress, CancellationToken cancellationToken);

        Task<List<IJob<CopyJobConfig>>> SaveCopyJobAsync(string name);

        Task<List<IJob<CopyJobConfig>>> DeleteJobAsync(string id);

        void SetDestination(string path);

        void SetSource(string path);

        void Clear();
    }
}
