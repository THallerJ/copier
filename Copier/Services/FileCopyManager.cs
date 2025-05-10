using CommunityToolkit.Mvvm.ComponentModel;
using Copier.Interfaces;
using Copier.Models;
using System.IO;

namespace Copier.Services
{
    public partial class FileCopyManager : ObservableObject, IFileCopyManager
    {
        private readonly IJsonJobFileHandler JsonJobFileHandler;
        private readonly IFileService FileService;
        private readonly IDirectoryService DirectoryService;
        private static readonly string CopyJobFileName = "copy_jobs";

        public List<IJob<CopyJobConfig>> CopyJobs { get; private set; }
        public CopyJob Job { get; set; } = new();

        private FileCopyManager(IJsonJobFileHandler jsonJobFileHandler, List<IJob<CopyJobConfig>> initialCopyJobs, IFileService fileService, IDirectoryService directoryService)
        {
            JsonJobFileHandler = jsonJobFileHandler;
            CopyJobs = initialCopyJobs;
            FileService = fileService;
            DirectoryService = directoryService;
        }

        public static IFileCopyManager Create(IJsonJobFileHandler jsonJobHandler, IFileService file, IDirectoryService directory)
        {
            var initialCopyJobs = jsonJobHandler.Read<CopyJobConfig>(CopyJobFileName);
            return new FileCopyManager(jsonJobHandler, initialCopyJobs, file, directory);
        }

        public void RunCopyJob(IProgress<float> progress, CancellationToken cancellationToken)
        {
            if (Job.Config.Src != null && Job.Config.Dest != null)
                RunCopyJob(Job.Config.Src, Job.Config.Dest, progress, cancellationToken);
        }

        public void RunCopyJob(string srcPath, string destPath, IProgress<float> progress, CancellationToken cancellationToken)
        {
            var files = DirectoryService.EnumerateFiles(srcPath, "*", SearchOption.AllDirectories);

            int fileCount = files.Count();
            int fileIndex = 0;

            foreach (string filePath in files)
            {
                if (cancellationToken.IsCancellationRequested) return;

                string relativePath = Path.GetRelativePath(srcPath, filePath);
                string destFilePath = Path.Combine(destPath, relativePath);

                var destDirPath = Path.GetDirectoryName(destFilePath);
                if (destDirPath != null)
                {
                    DirectoryService.CreateDirectory(destDirPath);
                }

                FileService.Copy(filePath, destFilePath, true);

                fileIndex++;
                progress.Report((int)((fileIndex / (float)fileCount) * 100));
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
