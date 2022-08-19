using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using SurveyExample2.Interfaces;

namespace SurveyExample2.Converters
{
    public class ElementTypeConverter<T> : JsonConverter<T> where T : IElementType
    {
        private readonly IEnumerable<Type> _types;

        public ElementTypeConverter()
        {
            var type = typeof(T);
            _types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToList();
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty(nameof(IElementType.Type), out var typeProperty))
                {
                    throw new JsonException();
                }

                var type = _types.FirstOrDefault(x => x.Name == typeProperty.GetString());
                if (type == null)
                {
                    throw new JsonException();
                }

                var jsonObject = jsonDocument.RootElement.GetRawText();
                var result = (T)JsonSerializer.Deserialize(jsonObject, type, options);

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }
    }
}