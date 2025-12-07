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
            if (!string.IsNullOrEmpty(dateString))
            {
                // Try parsing with different formats
                if (DateTime.TryParse(dateString, null, System.Globalization.DateTimeStyles.RoundtripKind, out var date))
                {
                    // If no timezone info, assume UTC
                    if (date.Kind == DateTimeKind.Unspecified)
                    {
                        return DateTime.SpecifyKind(date, DateTimeKind.Utc);
                    }
                    return date;
                }
            }
        }
        else if (reader.TokenType == JsonTokenType.Null)
        {
            return DateTime.UtcNow;
        }
        return DateTime.UtcNow;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O")); // ISO 8601 format
    }
}

