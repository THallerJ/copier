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

        public List<IJob<CopyJobConfig>> CopyJobs { get; private set; }
        public CopyJob Job { get; } = new();

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
            if (Job.Config.SrcPath != null && Job.Config.DestPath != null)
                RunCopyJob(Job.Config.SrcPath, Job.Config.DestPath);
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
            if (Job.Config.SrcPath != null && Job.Config.DestPath != null && id.Length > 0)
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

        public void SetDestPath(string path)
        {
            Job.Config.DestPath = path;
        }

        public void SetSrcPath(string path)
        {
            Job.Config.SrcPath = path;
        }
    }
}
