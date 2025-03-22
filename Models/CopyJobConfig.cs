namespace Copier.Models
{
    public class CopyJobConfig
    {
        public CopyJobConfig(string? srcPath, string? destPath)
        {
            SrcPath = srcPath;
            DestPath = destPath;
        }

        public CopyJobConfig() { }

        public string? SrcPath { get; set; }

        public string? DestPath { get; set; }
    }
}
