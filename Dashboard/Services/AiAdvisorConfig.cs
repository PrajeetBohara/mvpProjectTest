using System;

namespace Dashboard.Services;

/// <summary>
/// Central configuration for the AI Advisor experience.
/// Replace the placeholder URLs with your deployed endpoints.
/// </summary>
public static class AiAdvisorConfig
{
    /// <summary>
    /// Public web chat URL (Render.com) that the QR code will open.
    /// Replace with your Render URL: https://your-app-name.onrender.com
    /// </summary>
    public static string ChatUrl { get; set; } = "https://ai-advisor-api-wzez.onrender.com";

    /// <summary>
    /// API endpoint for transcript mirroring (Render.com).
    /// Replace with your Render URL: https://your-app-name.onrender.com
    /// </summary>
    public static string TranscriptEndpoint { get; set; } = "https://ai-advisor-api-wzez.onrender.com/api/transcript?sessionId=demo";

    /// <summary>
    /// Endpoint to clear a transcript session when leaving the page.
    /// Replace with your Render URL: https://your-app-name.onrender.com
    /// </summary>
    public static string ClearTranscriptEndpoint { get; set; } = "https://ai-advisor-api-wzez.onrender.com/api/transcript/clear";

    /// <summary>
    /// Degree catalog source URLs used for RAG (provided by the user).
    /// </summary>
    public static readonly string[] DegreeCatalogLinks =
    {
        "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59137&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
        "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59280&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
        "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59024&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx"
    };
}

