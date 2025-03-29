using CommunityToolkit.Mvvm.ComponentModel;
using Copier.Interfaces;
using Copier.Models;
using System.IO;

namespace Copier.Services
{
    public partial class FileCopyManager : ObservableObject, IFileCopyManager
    {
        private readonly IJsonJobFileHandler JsonJobHandler;
        private static readonly string CopyJobFileName = "copy_jobs";

        public List<IJob<CopyJobConfig>> CopyJobs { get; }
        public CopyJob Job { get; set; } = new();

        private FileCopyManager(IJsonJobFileHandler jsonJobHandler, List<IJob<CopyJobConfig>> initialCopyJobs)
        {
            JsonJobHandler = jsonJobHandler;
            CopyJobs = initialCopyJobs;
        }

        public static IFileCopyManager Create(IJsonJobFileHandler jsonJobHandler)
        {
            var initialCopyJobs = jsonJobHandler.Read<CopyJobConfig>(CopyJobFileName);
            return new FileCopyManager(jsonJobHandler, initialCopyJobs);
        }

        public void RunCopyJob()
        {
            if (Job.Config.Src != null && Job.Config.Dest != null)
                RunCopyJob(Job.Config.Src, Job.Config.Dest);
        }

        public void RunCopyJob(string srcPath, string destPath)
        {
            foreach (string filePath in Directory.GetFiles(srcPath))
            {
                string copiedFile = Path.Combine(destPath, Path.GetFileName(filePath));

                if (!File.Exists(copiedFile))
                {
                    File.Copy(filePath, copiedFile);
                }
            }

            foreach (string dirPath in Directory.GetDirectories(srcPath))
            {
                string copiedDir = Path.Combine(destPath, Path.GetFileName(dirPath));
                Directory.CreateDirectory(copiedDir);
                RunCopyJob(dirPath, copiedDir);
            }
        }

        public async Task<List<IJob<CopyJobConfig>>> SaveCopyJobAsync(string id)
        {
            if (Job.Config.Src != null && Job.Config.Dest != null && id.Length > 0)
            {
                Job.Id = id;
                return await JsonJobHandler.WriteAsync(CopyJobFileName, Job);
            }

            return [];
         }

        public Task GetCopyJobAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void SetDestination(string path)
        {
            Job.Config.Dest = path;
        }

        public void SetSource(string path)
        {
            Job.Config.Src = path;
        }
    }
}
