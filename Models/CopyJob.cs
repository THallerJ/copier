using Copier.Interfaces;
using System.Text.Json.Serialization;

namespace Copier.Models
{
    public class CopyJob : IJob<CopyJobConfig>
    {
        public string? Id { get; set; }

        public CopyJobConfig Config { get; }

        public CopyJob(string id, string srcPath, string destPath)
        {
            Id = id;
            Config = new CopyJobConfig(srcPath, destPath);
        }

        public CopyJob() {
            Config = new CopyJobConfig();
        }

        [JsonPropertyName("JobType")]
        public string JobType => "CopyJob";

    }
}