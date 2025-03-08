using Copier.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Copier.Converters
{
    public class JobConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(IJob<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type elementType = typeToConvert.GetGenericArguments()[0];
            Type converterType = typeof(JobConverter<>).MakeGenericType(elementType);

            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }
}