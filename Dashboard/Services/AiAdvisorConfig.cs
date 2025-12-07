using System;

namespace Dashboard.Services;

/// <summary>
/// Central configuration for the AI Advisor experience.
/// Replace the placeholder URLs with your deployed endpoints.
/// </summary>
public static class AiAdvisorConfig
{
    /// <summary>
    /// Public web chat URL hosted on Vercel (or similar) that the QR code will open.
    /// Example: https://your-ai-advisor.vercel.app
    /// </summary>
    public static string ChatUrl { get; set; } = "https://ai-advisor-for-encs-lng-dashboard.vercel.app";

    /// <summary>
    /// Simple API endpoint for transcript mirroring (runs locally or on a simple host).
    /// 
    /// IMPORTANT: If running MAUI app on Android/phone, use your computer's IP address instead of localhost!
    /// 
    /// To find your IP:
    /// - Windows: Run "ipconfig" in PowerShell, look for "IPv4 Address"
    /// - Example: "http://192.168.1.100:5000/api/transcript?sessionId=demo"
    /// 
    /// If MAUI app runs on the same computer as the API, localhost works fine.
    /// </summary>
    public static string TranscriptEndpoint { get; set; } = "http://192.168.1.11:5000/api/transcript?sessionId=demo";

    /// <summary>
    /// Endpoint to clear a transcript session when leaving the page.
    /// Use the same IP address as TranscriptEndpoint (replace localhost with your computer's IP if needed).
    /// </summary>
    public static string ClearTranscriptEndpoint { get; set; } = "http://192.168.1.11:5000/api/transcript/clear";

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

