namespace Copier.Models
{
    public class CopyJobConfig
    {
        public CopyJobConfig(string? fromPath, string? toPath)
        {
            FromPath = fromPath;
            ToPath = toPath;
        }

        public CopyJobConfig() { }

        public string? FromPath { get; set; }

        public string? ToPath { get; set; }
    }
}
