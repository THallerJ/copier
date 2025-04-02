using CommunityToolkit.Mvvm.ComponentModel;
using Copier.Interfaces;
using Copier.Models;
using System.IO;

namespace Copier.Services
{
    public partial class FileCopyManager : ObservableObject, IFileCopyManager
    {
        private readonly IJsonJobFileHandler JsonJobFileHandler;
        private static readonly string CopyJobFileName = "copy_jobs";

        public List<IJob<CopyJobConfig>> CopyJobs { get; private set; }
        public CopyJob Job { get; set; } = new();

        private FileCopyManager(IJsonJobFileHandler jsonJobFileHandler, List<IJob<CopyJobConfig>> initialCopyJobs)
        {
            JsonJobFileHandler = jsonJobFileHandler;
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
                return await JsonJobFileHandler.WriteAsync(CopyJobFileName, Job);
            }

            return CopyJobs;
         }

        public void SetDestination(string path)
        {
            Job.Config.Dest = path;
        }

        public void SetSource(string path)
        {
            Job.Config.Src = path;
        }

        public async Task<List<IJob<CopyJobConfig>>> DeleteJobAsync(string id)
        {
            CopyJobs = await JsonJobFileHandler.DeleteAsync<CopyJobConfig>(CopyJobFileName, id);
            return CopyJobs;
        }

        public void Clear()
        {
            JsonJobFileHandler.DeleteAllData();
            CopyJobs = [];
        }
    }
}
