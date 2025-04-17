using Copier.Factorys;
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

        private static string GetPath(string filename)
        {
            return Path.Combine(DefaultPath, $"{filename}.json");
        }

        public async Task<List<IJob<T>>> ReadAsync<T>(string filename)
        {
            return await ReadAsyncHelper<T>(filename, true);
        }

        public List<IJob<T>> Read<T>(string filename)
        {
            return ReadAsyncHelper<T>(filename, false).GetAwaiter().GetResult();
        }


        private async Task<List<IJob<T>>> ReadAsyncHelper<T>(string filename, bool isAsync)
        {
            string path = GetPath(filename);

            var list = new List<IJob<T>>();

            string fileJson;
            if (File.Exists(path))
            {
                if (isAsync)
                {
                    fileJson = await File.ReadAllTextAsync(path);
                }
                else
                {
                    fileJson = File.ReadAllText(path);
                }

                if (!string.IsNullOrWhiteSpace(fileJson))
                {
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

        public async Task<List<IJob<T>>> DeleteAsync<T>(string filename, string id)
        {
            var path = GetPath(filename);
            if (File.Exists(path) == false) return [];

            var list = await ReadAsync<T>(filename);
            var job = list.FirstOrDefault(item => item.Id == item.Id);

            if (job != null)
            {
                list.Remove(job);
                var json = JsonSerializer.Serialize(list, Options);
                await File.WriteAllTextAsync(path, json);
            }

            return list;
        }
    }
}
