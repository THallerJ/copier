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
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("JobType", out JsonElement jobTypeElement))
                {
                    string? jobType = jobTypeElement.GetString();
                    if (jobType == null)
                    {
                        throw new JsonException("JobType property is null");
                    }

                    Type concreteType = jobType switch
                    {
                        "CopyJob" => typeof(CopyJob),
                        _ => throw new NotSupportedException($"Job type '{jobType}' is not supported")
                    };

                    var deserializedObject = JsonSerializer.Deserialize(root.GetRawText(), concreteType, options);
                    if (deserializedObject == null)
                    {
                        throw new JsonException($"Deserialization returned null for job type '{jobType}'");
                    }

                    return (IJob<T>)deserializedObject;
                }

                throw new JsonException("Missing JobType discriminator");
            }
        }

        public override void Write(Utf8JsonWriter writer, IJob<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}