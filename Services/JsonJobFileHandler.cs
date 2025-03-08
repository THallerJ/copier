using Copier.Converters;
using Copier.Interfaces;
using System.IO;
using System.Text.Json;

namespace Copier.Services
{
    public class JsonJobFileHandler : IJsonJobFileHandler
    {
        private static readonly string DefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "copier");

        private readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            Converters =
            {
                new JobConverterFactory()
            }
        };

        public async Task<List<IJob<T>>> ReadAsync<T>(string filename)
        {
            string path = GetPath(filename);

            var list = new List<IJob<T>>();

            if (File.Exists(path))
            {
                var fileJson = await File.ReadAllTextAsync(path);
                return list = JsonSerializer.Deserialize<List<IJob<T>>>(fileJson, Options) ?? new List<IJob<T>>();
            }

            return [];
        }

        public async Task WriteAsync<T>(string filename, IJob<T> data)
        {
            if (data.Id == null) return;

            if (Directory.Exists(DefaultPath) == false)
            {
                Directory.CreateDirectory(DefaultPath);
            }

            string path = GetPath(filename);

            var list = await ReadAsync<T>(filename);

            if (list.Any(item => item.Id == data.Id)) return;

            list.Add(data);
            var json = JsonSerializer.Serialize(list, Options);
            await File.WriteAllTextAsync(path, json);
        }

        public void DeleteAllData()
        {
            Directory.Delete(DefaultPath, true);
        }

        private static string GetPath(string filename)
        {
            return Path.Combine(DefaultPath, $"{filename}.json");
        }
    }
}
