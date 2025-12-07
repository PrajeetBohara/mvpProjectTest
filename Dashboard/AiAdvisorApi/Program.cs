using System.Collections.Concurrent;
using OpenAI.Chat;
using OpenAI.Embeddings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(); // For fetching catalog links

// Cloud providers set the port via PORT environment variable
// Render uses port 10000, Railway uses 8080, local dev uses 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// Serve static files (HTML page)
app.UseDefaultFiles();
app.UseStaticFiles();

// OpenAI client
var openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if (string.IsNullOrWhiteSpace(openAiApiKey))
{
    Console.WriteLine("WARNING: OPENAI_API_KEY not set. AI features will not work.");
}
var chatClient = openAiApiKey != null ? new ChatClient(openAiApiKey) : null;
var embeddingClient = openAiApiKey != null ? new EmbedClient(openAiApiKey) : null;

// Degree catalog links for RAG context
var degreeCatalogLinks = new[]
{
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59137&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59280&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59024&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx"
};

// In-memory transcript storage (keyed by sessionId)
var transcripts = new ConcurrentDictionary<string, List<AiAdvisorMessage>>();

// Handle chat - call OpenAI and store Q&A
app.MapPost("/api/chat", async (ChatRequest req, IHttpClientFactory httpClientFactory) =>
{
    var sessionId = string.IsNullOrWhiteSpace(req.SessionId) ? "demo" : req.SessionId;
    var messages = transcripts.GetOrAdd(sessionId, _ => new List<AiAdvisorMessage>());

    if (string.IsNullOrWhiteSpace(req.Question))
    {
        return Results.BadRequest("Question is required");
    }

    Console.WriteLine($"[API] Received POST /api/chat for session: {sessionId}");
    Console.WriteLine($"[API] Question: {req.Question}");

    // Add user message
    messages.Add(new AiAdvisorMessage
    {
        Role = "user",
        Content = req.Question,
        Timestamp = DateTime.UtcNow
    });

    string answer = "I'm sorry, but the OpenAI API key is not configured. Please set OPENAI_API_KEY environment variable.";

    // Call OpenAI if configured
    if (chatClient != null && embeddingClient != null)
    {
        try
        {
            // Simple RAG: fetch context from degree catalog links
            var contextParts = new List<string>();
            var httpClient = httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            foreach (var link in degreeCatalogLinks.Take(1)) // Use first link for simplicity
            {
                try
                {
                    var content = await httpClient.GetStringAsync(link);
                    // Extract text content (simplified - in production, use proper HTML parsing)
                    var text = System.Text.RegularExpressions.Regex.Replace(content, "<[^>]+>", " ");
                    var chunks = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(c => c.Trim().Length > 50)
                        .Take(5)
                        .ToList();
                    contextParts.AddRange(chunks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[API] Error fetching {link}: {ex.Message}");
                }
            }

            var context = string.Join("\n", contextParts.Take(10));

            // Build messages for OpenAI
            var openAiMessages = new List<Message>
            {
                new Message(Role.System, "You are an academic advisor for McNeese State University. Provide degree plan advice based on the provided context. Always cite your sources. If you cannot answer from the context, state that you don't have enough information."),
                new Message(Role.User, $"Context from degree catalog:\n{context}\n\nQuestion: {req.Question}")
            };

            // Add conversation history
            foreach (var msg in messages.TakeLast(4).Where(m => m.Role == "user" || m.Role == "assistant"))
            {
                var role = msg.Role == "user" ? Role.User : Role.Assistant;
                openAiMessages.Add(new Message(role, msg.Content));
            }

            var chatResponse = await chatClient.CompleteChatAsync(openAiMessages);
            answer = chatResponse.Value.Content[0].Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[API] OpenAI error: {ex.Message}");
            answer = $"Error: {ex.Message}";
        }
    }

    // Add assistant message
    messages.Add(new AiAdvisorMessage
    {
        Role = "assistant",
        Content = answer,
        Timestamp = DateTime.UtcNow
    });

    Console.WriteLine($"[API] Added Q&A. Total messages: {messages.Count}");

    return Results.Ok(new { answer, count = messages.Count });
});

// Get transcript for a session
app.MapGet("/api/transcript", (string? sessionId) =>
{
    sessionId ??= "demo";
    var messages = transcripts.GetOrAdd(sessionId, _ => new List<AiAdvisorMessage>());
    Console.WriteLine($"[API] GET /api/transcript for session: {sessionId}, returning {messages.Count} messages");
    return Results.Ok(messages);
});

// Clear transcript for a session
app.MapPost("/api/transcript/clear", (ClearRequest? req) =>
{
    var sessionId = req?.SessionId ?? "demo";
    transcripts.TryRemove(sessionId, out _);
    return Results.Ok(new { ok = true, sessionId });
});

// Log startup info
var railwayDomain = Environment.GetEnvironmentVariable("RAILWAY_PUBLIC_DOMAIN");
var renderUrl = Environment.GetEnvironmentVariable("RENDER_EXTERNAL_URL");
var url = railwayDomain != null ? $"https://{railwayDomain}"
    : renderUrl ?? $"http://localhost:{port}";
Console.WriteLine("========================================");
Console.WriteLine("AI Advisor API is running!");
Console.WriteLine($"Listening on port: {port}");
if (url.StartsWith("https://"))
{
    Console.WriteLine($"Public URL: {url}");
    Console.WriteLine("Deployed online - ready to use!");
}
else
{
    Console.WriteLine($"Local URL: {url}");
    Console.WriteLine("For online deployment, see DEPLOY_ONLINE.md");
}
Console.WriteLine("========================================");

app.Run();

// Models (matching Dashboard.Models.AiAdvisorMessage)
public class AiAdvisorMessage
{
    public string Role { get; set; } = "assistant"; // "user" | "assistant"
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

record ChatRequest
{
    public string? SessionId { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
}

record ClearRequest
{
    public string? SessionId { get; set; }
}

