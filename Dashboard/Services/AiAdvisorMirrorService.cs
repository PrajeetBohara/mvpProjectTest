using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Polls a transcript endpoint to mirror AI advisor responses inside the MAUI app.
/// The endpoint is expected to return a JSON array of AiAdvisorMessage.
/// </summary>
public class AiAdvisorMirrorService
{
    private readonly HttpClient _httpClient;

    public AiAdvisorMirrorService(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<IReadOnlyList<AiAdvisorMessage>> GetTranscriptAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(AiAdvisorConfig.TranscriptEndpoint))
        {
            return Array.Empty<AiAdvisorMessage>();
        }

        try
        {
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Fetching transcript from: {AiAdvisorConfig.TranscriptEndpoint}");
            
            // Get raw JSON first to debug
            var response = await _httpClient.GetAsync(AiAdvisorConfig.TranscriptEndpoint, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Raw JSON response: {jsonString}");
            
            // Configure JSON options - use default (PascalCase matching)
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false, // API returns PascalCase, model expects PascalCase
                Converters = { new Models.DateTimeJsonConverter() }
            };
            
            var result = JsonSerializer.Deserialize<List<AiAdvisorMessage>>(jsonString, jsonOptions);
            
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Deserialized {result?.Count ?? 0} messages");
            if (result != null)
            {
                foreach (var msg in result)
                {
                    System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Message: Role={msg.Role}, Content={msg.Content?.Substring(0, Math.Min(50, msg.Content?.Length ?? 0))}...");
                }
            }
            return result ?? new List<AiAdvisorMessage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Error fetching transcript from {AiAdvisorConfig.TranscriptEndpoint}");
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Error type: {ex.GetType().Name}");
            System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Error message: {ex.Message}");
            if (ex.InnerException != null)
            {
                System.Diagnostics.Debug.WriteLine($"[AiAdvisorMirrorService] Inner exception: {ex.InnerException.Message}");
            }
            return Array.Empty<AiAdvisorMessage>();
        }
    }

    public async Task ClearTranscriptAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(AiAdvisorConfig.ClearTranscriptEndpoint))
            return;

        try
        {
            var request = new { sessionId = "demo" };
            await _httpClient.PostAsJsonAsync(AiAdvisorConfig.ClearTranscriptEndpoint, request, cancellationToken);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Transcript clear error: {ex.Message}");
        }
    }
}

