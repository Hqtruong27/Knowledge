using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Knowledge.Common.Helper
{
    public static class Converter
    {
        public static int ToInt<T>(this T input)
        {
            Int32 output = 404;
            if (input is null) return output;
            output = Convert.ToInt32(input);
            return output;
        }
    }
    /// <summary>
    /// Ex: var serializeOptions = new JsonSerializeOptions();
    ///     serializeOptions.Converters.Add(new StringConverter());
    ///     var deserializeJson = JsonSerializer.Deserialize(jsonOutput, serializeOptions);
    /// </summary>
    public class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var stringValue = reader.GetInt32();
                return stringValue.ToString();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

}
