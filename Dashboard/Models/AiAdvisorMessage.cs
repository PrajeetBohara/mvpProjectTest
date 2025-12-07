using System;

namespace Dashboard.Models;

/// <summary>
/// Represents a single AI advisor exchange for mirroring on the MAUI page.
/// </summary>
public class AiAdvisorMessage
{
    public string Role { get; set; } = "assistant"; // "user" | "assistant"
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

