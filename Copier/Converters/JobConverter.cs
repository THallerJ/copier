using Copier.Interfaces;
using Copier.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Copier.Converters
{
    public class JobConverter<T> : JsonConverter<IJob<T>>
    {
        public override IJob<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("JobType", out JsonElement jobTypeElement))
            {
                string? jobType = jobTypeElement.GetString();
                if (jobType == null)
                {
                    throw new JsonException("JobType property is null");
                }

                Type concreteType = GetConcreteType(jobType);
                var deserializedObject = DeserializeJob(root, concreteType, options);

                if (deserializedObject == null)
                {
                    throw new JsonException($"Deserialization returned null for job type '{jobType}'");
                }

                return (IJob<T>)deserializedObject;
            }

            throw new JsonException("Missing JobType discriminator");
        }

        public override void Write(Utf8JsonWriter writer, IJob<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static Type GetConcreteType(string jobType)
        {
            return jobType switch
            {
                "CopyJob" => typeof(CopyJob),
                _ => throw new NotSupportedException($"Job type '{jobType}' is not supported")
            };
        }

        private static object? DeserializeJob(JsonElement root, Type concreteType, JsonSerializerOptions options)
        {
            var deserializedObject = JsonSerializer.Deserialize(root.GetRawText(), concreteType, options);

            switch (deserializedObject)
            {
                case CopyJob copyJob:
                    if (root.TryGetProperty("Config", out JsonElement configElement))
                    {
                        var config = JsonSerializer.Deserialize<CopyJobConfig>(configElement.GetRawText(), options);
                        if (config != null)
                        {
                            copyJob.Config.Src = config.Src;
                            copyJob.Config.Dest = config.Dest;
                        }
                    }
                    return deserializedObject;
                default:
                    throw new NotSupportedException($"Job type '{concreteType.Name}' is not supported");
            }
        }
    }
}