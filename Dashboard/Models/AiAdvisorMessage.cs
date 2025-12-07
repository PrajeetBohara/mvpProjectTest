using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dashboard.Models;

/// <summary>
/// Represents a single AI advisor exchange for mirroring on the MAUI page.
/// </summary>
public class AiAdvisorMessage
{
    [JsonPropertyName("Role")]
    public string Role { get; set; } = "assistant"; // "user" | "assistant"
    
    [JsonPropertyName("Content")]
    public string Content { get; set; } = string.Empty;
    
    [JsonPropertyName("Timestamp")]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Custom JSON converter to handle ISO 8601 timestamp strings from Python API.
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateString = reader.GetString();
            if (DateTime.TryParse(dateString, out var date))
            {
                return date;
            }
        }
        return DateTime.UtcNow;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O")); // ISO 8601 format
    }
}

