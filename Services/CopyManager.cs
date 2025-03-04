using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public partial class CopyManager : ICopyManager
    {
        public string? Name { get; set; }

        public string? FromPath { get; set; }

        public string? ToPath { get; set; }

        public void runCopy()
        {
            if (FromPath != null && ToPath != null)
                runCopy(FromPath, ToPath);
        }

        public void runCopy(string fromPath, string toPath)
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
                runCopy(dirPath, copiedDir);
            }
        }

        public void SaveCopy(string name)
        {
            throw new NotImplementedException();
        }

        public void GetCopy(string name)
        {
            throw new NotImplementedException();
        }
    }
}
