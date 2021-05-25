using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core
{
  internal class JsonConverterForNullableDateTimeOffset : JsonConverter<DateTimeOffset?>
  {
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetString() == "" ? null : reader.GetDateTimeOffset();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
      if (!value.HasValue)
      {
        writer.WriteStringValue("");
      }
      else
      {
        writer.WriteStringValue(value.Value);
      }
    }
  }
}