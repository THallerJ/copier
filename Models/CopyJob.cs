using Copier.Interfaces;
using System.Text.Json.Serialization;

namespace Copier.Models
{
    public class CopyJob : IJob<CopyJobConfig>
    {
        public string? Id { get; set; }

        public CopyJobConfig Config { get; }

        public CopyJob(string id, string fromPath, string toPath)
        {
            Id = id;
            Config = new CopyJobConfig(fromPath, toPath);
        }

        public CopyJob() {
            Config = new CopyJobConfig();
        }

        [JsonPropertyName("JobType")]
        public string JobType => "CopyJob";

    }

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