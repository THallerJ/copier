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

        public FileCopyManager(IJsonJobFileHandler jsonJobHandler)
        {
            JsonJobHandler = jsonJobHandler;
        }

        public CopyJob Job { get; } = new();

        public void RunCopyJob()
        {
            if (Job.Config.FromPath != null && Job.Config.ToPath != null)
                RunCopyJob(Job.Config.FromPath, Job.Config.ToPath);
        }

        public void RunCopyJob(string fromPath, string toPath)
        {
            foreach (string filePath in Directory.GetFiles(fromPath))
            {
                string copiedFile = Path.Combine(toPath, Path.GetFileName(filePath));

                if (!File.Exists(copiedFile))
                {
                    File.Copy(filePath, copiedFile);
                }
            }

            foreach (string dirPath in Directory.GetDirectories(fromPath))
            {
                string copiedDir = Path.Combine(toPath, Path.GetFileName(dirPath));
                Directory.CreateDirectory(copiedDir);
                RunCopyJob(dirPath, copiedDir);
            }
        }

        public async Task SaveCopyJobAsync(string id)
        {
            if (Job.Config.FromPath != null && Job.Config.ToPath != null && id.Length > 0)
            {
                Job.Id = id;
                await JsonJobHandler.WriteAsync(CopyJobFileName, Job);
            }
         }

        public Task GetCopyJobAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void SetToPath(string path)
        {
            Job.Config.ToPath = path;
        }

        public void SetFromPath(string path)
        {
            Job.Config.FromPath = path;
        }
    }
}
