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
            return await ReadInternalAsync<T>(filename, true);
        }

        public List<IJob<T>> Read<T>(string filename)
        {
            return ReadInternalAsync<T>(filename, false).GetAwaiter().GetResult();
        }


        private async Task<List<IJob<T>>> ReadInternalAsync<T>(string filename, bool isAsync)
        {
            string path = GetPath(filename);

            var list = new List<IJob<T>>();

            if (File.Exists(path))
            {
                if (isAsync)
                {
                    var fileJson = await File.ReadAllTextAsync(path);
                    list = JsonSerializer.Deserialize<List<IJob<T>>>(fileJson, Options) ?? new List<IJob<T>>();
                }
                else
                {
                    var fileJson = File.ReadAllText(path);
                    list = JsonSerializer.Deserialize<List<IJob<T>>>(fileJson, Options) ?? new List<IJob<T>>();
                }
            }

            return list;
        }

        public async Task<List<IJob<T>>> WriteAsync<T>(string filename, IJob<T> data)
        {
            if (Directory.Exists(DefaultPath) == false)
            {
                Directory.CreateDirectory(DefaultPath);
            }

            string path = GetPath(filename);

            var list = await ReadAsync<T>(filename);

            if (data.Id == null || list.Any(item => item.Id == data.Id))
            {
                return list;
            }

            list.Add(data);
            var json = JsonSerializer.Serialize(list, Options);
            await File.WriteAllTextAsync(path, json);

            return list;
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
