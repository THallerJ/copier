namespace Copier.Models
{
    public class CopyJobConfig
    {
        public CopyJobConfig(string? src, string? dest)
        {
            Src = src;
            Dest = dest;
        }

        public CopyJobConfig() { }

        public string? Src { get; set; }

        public string? Dest { get; set; }
    }
}
