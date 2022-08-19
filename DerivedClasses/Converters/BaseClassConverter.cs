using DerivedClasses.Classes;
using DerivedClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace DerivedClasses.Converters
{
    public class BaseClassConverter : JsonConverter<BaseClass>
    {
        public override bool CanConvert(Type type)
        {
            return typeof(BaseClass).IsAssignableFrom(type);
        }

        public override BaseClass Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || reader.GetString() != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            BaseClass baseClass;
            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            switch (typeDiscriminator)
            {
                case TypeDiscriminator.DerivedA:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    baseClass = (DerivedA)JsonSerializer.Deserialize(ref reader, typeof(DerivedA));
                    break;
                case TypeDiscriminator.DerivedB:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    baseClass = (DerivedB)JsonSerializer.Deserialize(ref reader, typeof(DerivedB));
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            return baseClass;
        }

        public override void Write(
            Utf8JsonWriter writer,
            BaseClass value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is DerivedA derivedA)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DerivedA);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, derivedA);
            }
            else if (value is DerivedB derivedB)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DerivedB);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, derivedB);
            }
            else
            {
                throw new NotSupportedException();
            }

            writer.WriteEndObject();
        }
    }
}