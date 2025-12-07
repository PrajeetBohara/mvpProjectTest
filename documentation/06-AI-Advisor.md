# AI Advisor - Complete Guide

This document explains the AI Advisor system, including the Flask API, React/Next.js web interface, and how they integrate with the .NET MAUI app.

---

## What is the AI Advisor?

The **AI Advisor** is an intelligent chatbot that helps students with:
- **Degree planning** - What courses to take
- **Academic advice** - Graduation requirements
- **Course recommendations** - Based on student progress

**How it works**:
1. Student asks a question on their phone (web interface)
2. Question is sent to Flask API
3. Flask API uses OpenAI to generate answer
4. Answer is stored in transcript
5. MAUI app displays the conversation on the dashboard

---

## Architecture Overview

```
Student's Phone
    ↓
Next.js Web App (Vercel)
    ↓
Flask API (Render)
    ↓
OpenAI API
    ↓
Flask API stores transcript
    ↓
MAUI App polls Flask API
    ↓
Dashboard displays conversation
```

**Three Components**:
1. **Next.js Web App** - Student interface (on phone)
2. **Flask API** - Handles chat logic (on Render)
3. **MAUI App** - Displays conversation (on dashboard)

---

## Part 1: Flask API (Backend)

### What is Flask?

**Flask** is a Python web framework. Think of it as:
- **A waiter** - Takes your order (request) and brings food (response)
- **A server** - Handles HTTP requests and responses
- **Simple and lightweight** - Easy to set up and use

### Project Structure

**Location**: `Dashboard/AiAdvisorApi/`

```
AiAdvisorApi/
├── app.py              # Main Flask application
├── requirements.txt    # Python dependencies
└── wwwroot/            # Static files (HTML, if needed)
```

### The Flask App (app.py)

#### Basic Structure

```python
from flask import Flask, request, jsonify
from flask_cors import CORS
import os
from openai import OpenAI

app = Flask(__name__)
CORS(app)  # Allow requests from other domains

# Initialize OpenAI client
openai_key = os.getenv('OPENAI_API_KEY')
openai_client = OpenAI(api_key=openai_key)

# In-memory storage (for demo - use database in production)
transcripts = {}  # Store conversations
last_updated = {}  # Track when conversations were updated
```

**What this does**:
- Creates Flask app
- Enables CORS (Cross-Origin Resource Sharing) - allows web app to call API
- Initializes OpenAI client
- Creates storage dictionaries (in-memory, for simplicity)

#### Chat Endpoint

```python
@app.route('/api/chat', methods=['POST'])
def chat():
    # Get data from request
    data = request.json
    session_id = data.get('sessionId', 'demo')
    question = data.get('question', '').strip()
    
    if not question:
        return jsonify({'error': 'Question required'}), 400
    
    # Add user message to transcript
    if session_id not in transcripts:
        transcripts[session_id] = []
    
    transcripts[session_id].append({
        'Role': 'user',
        'Content': question,
        'Timestamp': datetime.utcnow().isoformat()
    })
    
    # Get AI answer
    answer = "I'm sorry, but the OpenAI API key is not configured."
    
    if openai_client:
        try:
            # Prepare messages for OpenAI
            messages = [
                {"role": "system", "content": "You are an academic advisor..."},
                {"role": "user", "content": question}
            ]
            
            # Add conversation history (last 4 messages)
            for msg in transcripts[session_id][-4:]:
                role = msg.get('Role', '').lower()
                if role in ['user', 'assistant']:
                    messages.append({"role": role, "content": msg.get('Content', '')})
            
            # Call OpenAI
            response = openai_client.chat.completions.create(
                model="gpt-4o-mini",
                messages=messages
            )
            answer = response.choices[0].message.content
        except Exception as e:
            answer = f"Error: {str(e)}"
    
    # Add assistant message
    transcripts[session_id].append({
        'Role': 'assistant',
        'Content': answer,
        'Timestamp': datetime.utcnow().isoformat()
    })
    
    # Update last updated timestamp
    last_updated[session_id] = datetime.utcnow().isoformat()
    
    return jsonify({'answer': answer, 'count': len(transcripts[session_id])})
```

**Breaking it down**:

1. **Receive Request**:
   ```python
   data = request.json
   session_id = data.get('sessionId', 'demo')
   question = data.get('question', '')
   ```
   - Gets JSON data from request
   - Extracts session ID and question

2. **Store User Message**:
   ```python
   transcripts[session_id].append({
       'Role': 'user',
       'Content': question,
       'Timestamp': datetime.utcnow().isoformat()
   })
   ```
   - Adds user's question to transcript
   - Uses session ID to separate conversations

3. **Call OpenAI**:
   ```python
   response = openai_client.chat.completions.create(
       model="gpt-4o-mini",
       messages=messages
   )
   answer = response.choices[0].message.content
   ```
   - Sends messages to OpenAI
   - Gets AI-generated answer
   - Uses `gpt-4o-mini` (cheaper, faster model)

4. **Store Answer**:
   ```python
   transcripts[session_id].append({
       'Role': 'assistant',
       'Content': answer,
       'Timestamp': datetime.utcnow().isoformat()
   })
   ```
   - Adds AI answer to transcript

5. **Return Response**:
   ```python
   return jsonify({'answer': answer, 'count': len(transcripts[session_id])})
   ```
   - Returns JSON response
   - Includes answer and message count

#### Transcript Endpoint

```python
@app.route('/api/transcript')
def get_transcript():
    session_id = request.args.get('sessionId', 'demo')
    
    # Get transcript for session
    result = []
    for msg in transcripts.get(session_id, []):
        result.append({
            'Role': msg.get('Role', 'assistant'),
            'Content': msg.get('Content', ''),
            'Timestamp': msg.get('Timestamp', datetime.utcnow().isoformat())
        })
    
    return jsonify(result)
```

**What this does**:
- Returns all messages for a session
- Used by MAUI app to display conversation

#### Last Updated Endpoint

```python
@app.route('/api/last-updated')
def get_last_updated():
    session_id = request.args.get('sessionId', 'demo')
    return jsonify({
        'lastUpdated': last_updated.get(session_id, datetime.utcnow().isoformat())
    })
```

**What this does**:
- Returns timestamp of last message
- Used by MAUI app to check if new messages exist
- Enables efficient polling (only refresh when timestamp changes)

#### Clear Transcript Endpoint

```python
@app.route('/api/transcript/clear', methods=['POST'])
def clear_transcript():
    session_id = request.json.get('sessionId', 'demo') if request.json else 'demo'
    
    if session_id in transcripts:
        del transcripts[session_id]
    if session_id in last_updated:
        del last_updated[session_id]
    
    return jsonify({'ok': True, 'sessionId': session_id})
```

**What this does**:
- Clears conversation for a session
- Used when user starts a new conversation

---

## Part 2: Next.js Web App (Student Interface)

### Project Structure

**Location**: `Dashboard/ai-advisor-web/`

```
ai-advisor-web/
├── app/
│   ├── page.tsx              # Main chat interface
│   ├── layout.tsx            # App layout
│   └── api/
│       ├── chat/route.ts     # API route (calls Flask)
│       └── transcript/route.ts
├── lib/
│   ├── model.ts              # OpenAI client
│   ├── vectorStore.ts        # Vector search (RAG)
│   └── ingest.ts             # Document ingestion
└── package.json
```

### Main Chat Interface (page.tsx)

```tsx
"use client";

import { useState } from "react";

export default function Home() {
  const [question, setQuestion] = useState("");
  const [loading, setLoading] = useState(false);
  const [status, setStatus] = useState("");

  async function ask() {
    if (!question.trim()) return;
    
    setLoading(true);
    setStatus("Sending...");

    const userQuestion = question.trim();

    // Call Next.js API route
    const resp = await fetch("/api/chat", {
      method: "POST",
      body: JSON.stringify({ question: userQuestion, sessionId: "demo" }),
      headers: { "Content-Type": "application/json" }
    });

    if (!resp.body) {
      setLoading(false);
      return;
    }

    // Stream response
    const reader = resp.body.getReader();
    const decoder = new TextDecoder();
    let answer = "";

    while (true) {
      const { value, done } = await reader.read();
      if (done) break;
      const chunk = decoder.decode(value, { stream: true });
      answer += chunk;
    }

    // Send Q&A to Flask API
    const apiUrl = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5000";
    await fetch(`${apiUrl}/api/chat`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ sessionId: "demo", question: userQuestion, answer })
    });

    setLoading(false);
    setQuestion("");
    setStatus("Sent to advisor");
  }

  return (
    <main>
      <h1>AI Advisor</h1>
      <p>Ask about degree plans. Answers will show on the display.</p>

      <textarea
        value={question}
        onChange={(e) => setQuestion(e.target.value)}
        rows={4}
        placeholder="Example: I am in Applied CS, 45 credits done..."
      />

      <button onClick={ask} disabled={loading || !question.trim()}>
        {loading ? "Working..." : "Ask"}
      </button>

      <div>{status || "Messages display on the TV app."}</div>
    </main>
  );
}
```

**What this does**:
1. User types question
2. Clicks "Ask" button
3. Calls Next.js API route (`/api/chat`)
4. Next.js API calls OpenAI (with RAG - Retrieval Augmented Generation)
5. Streams answer back
6. Sends Q&A to Flask API (stores in transcript)
7. MAUI app polls and displays

### Next.js API Route (chat/route.ts)

```typescript
import { NextRequest } from "next/server";
import { openai, SYSTEM_PROMPT } from "../../../lib/model";
import { ensureIngested } from "../../../lib/ingest";
import { search } from "../../../lib/vectorStore";

export async function POST(req: NextRequest) {
  const body = await req.json();
  const question: string = body?.question ?? "";

  if (!question.trim()) {
    return new Response("Missing question", { status: 400 });
  }

  // Ensure documents are ingested (RAG)
  await ensureIngested();

  // Create embedding for question
  const qEmbedding = (
    await openai.embeddings.create({
      model: "text-embedding-3-small",
      input: question
    })
  ).data[0].embedding;

  // Search for relevant context
  const hits = search(qEmbedding, 6);
  const context = hits.map((h) => `Source: ${h.source}\n${h.text}`).join("\n\n");

  // Prepare messages with context
  const messages = [
    { role: "system" as const, content: SYSTEM_PROMPT },
    {
      role: "user" as const,
      content: `Context:\n${context}\n\nQuestion: ${question}`
    }
  ];

  // Stream response from OpenAI
  const completion = await openai.chat.completions.create({
    model: "gpt-4o-mini",
    messages,
    stream: true
  });

  const encoder = new TextEncoder();
  const stream = new ReadableStream({
    async start(controller) {
      for await (const part of completion) {
        const delta = part.choices[0]?.delta?.content ?? "";
        if (delta) {
          controller.enqueue(encoder.encode(delta));
        }
      }
      controller.close();
    }
  });

  return new Response(stream, {
    headers: {
      "Content-Type": "text/plain; charset=utf-8",
      "Cache-Control": "no-cache"
    }
  });
}
```

**What this does**:
1. **RAG (Retrieval Augmented Generation)**:
   - Creates embedding for question
   - Searches vector store for relevant documents
   - Includes context in prompt

2. **Streams Response**:
   - Sends answer as it's generated (not all at once)
   - Better user experience (sees answer appear)

---

## Part 3: MAUI App Integration

### AiAdvisorPage

**File**: `Dashboard/Pages/AiAdvisorPage.xaml` / `AiAdvisorPage.xaml.cs`

#### How It Works

```csharp
public partial class AiAdvisorPage : ContentPage
{
    private readonly AiAdvisorMirrorService _mirrorService;
    private readonly AiAdvisorViewModel _vm;
    private CancellationTokenSource? _pollCts;

    public AiAdvisorPage()
    {
        InitializeComponent();
        _mirrorService = new AiAdvisorMirrorService();
        _vm = new AiAdvisorViewModel(_mirrorService);
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _pollCts = new CancellationTokenSource();
        
        // Start polling for updates
        _ = _vm.StartSmartRefreshAsync(_mirrorService, _pollCts.Token, 1000, (time) => { });
        await _vm.RefreshAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _pollCts?.Cancel();  // Stop polling when leaving page
        _ = _vm.ClearSessionAsync();
    }
}
```

**What this does**:
- Creates service and view model
- Starts polling when page appears
- Stops polling when page disappears

#### Smart Polling

```csharp
public async Task StartSmartRefreshAsync(
    AiAdvisorMirrorService mirrorService, 
    CancellationToken token, 
    int intervalMs,
    Action<string?> updateLastKnownTime)
{
    string? lastKnownTime = null;
    
    while (!token.IsCancellationRequested)
    {
        try
        {
            // Check if there's a new update
            var currentLastUpdated = await mirrorService.GetLastUpdatedAsync(token);
            
            // Only refresh if the timestamp changed
            if (currentLastUpdated != null && currentLastUpdated != lastKnownTime)
            {
                lastKnownTime = currentLastUpdated;
                await RefreshAsync();  // Fetch new messages
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in smart refresh: {ex.Message}");
        }
        
        await Task.Delay(intervalMs, token);  // Wait 1 second
    }
}
```

**Why this is smart**:
- Checks timestamp first (lightweight)
- Only fetches full transcript if timestamp changed
- Reduces API calls and bandwidth

#### AiAdvisorMirrorService

**File**: `Dashboard/Services/AiAdvisorMirrorService.cs`

```csharp
public class AiAdvisorMirrorService
{
    private readonly HttpClient _httpClient;

    public async Task<IReadOnlyList<AiAdvisorMessage>> GetTranscriptAsync()
    {
        var response = await _httpClient.GetAsync(AiAdvisorConfig.TranscriptEndpoint);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        var result = JsonSerializer.Deserialize<List<AiAdvisorMessage>>(jsonString);
        return result ?? new List<AiAdvisorMessage>();
    }

    public async Task<string?> GetLastUpdatedAsync()
    {
        var response = await _httpClient.GetAsync(AiAdvisorConfig.LastUpdatedEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("lastUpdated", out var lastUpdated))
            {
                return lastUpdated.GetString();
            }
        }
        return null;
    }
}
```

**What this does**:
- Fetches transcript from Flask API
- Fetches last updated timestamp
- Parses JSON responses

---

## Configuration

### AiAdvisorConfig

**File**: `Dashboard/Services/AiAdvisorConfig.cs`

```csharp
public static class AiAdvisorConfig
{
    public static string ChatUrl { get; set; } = "https://ai-advisor-api-wzez.onrender.com";
    public static string TranscriptEndpoint { get; set; } = "https://ai-advisor-api-wzez.onrender.com/api/transcript?sessionId=demo";
    public static string LastUpdatedEndpoint { get; set; } = "https://ai-advisor-api-wzez.onrender.com/api/last-updated?sessionId=demo";
    public static string ClearTranscriptEndpoint { get; set; } = "https://ai-advisor-api-wzez.onrender.com/api/transcript/clear";
}
```

**What this does**:
- Stores all API endpoints
- Easy to update when deploying to different URLs

---

## Deployment

### Deploying Flask API to Render

1. **Create Render Account**:
   - Go to https://render.com/
   - Sign up (free)

2. **Create New Web Service**:
   - Click "New" → "Web Service"
   - Connect GitHub repository
   - Select `AiAdvisorApi` folder

3. **Configure**:
   - **Name**: `ai-advisor-api`
   - **Environment**: `Python 3`
   - **Build Command**: `pip install -r requirements.txt`
   - **Start Command**: `python app.py`

4. **Set Environment Variables**:
   - `OPENAI_API_KEY` - Your OpenAI API key
   - `PORT` - Usually `5000` (Render sets this automatically)

5. **Deploy**:
   - Click "Create Web Service"
   - Wait for deployment
   - Get your URL: `https://your-app.onrender.com`

6. **Update Config**:
   - Update `AiAdvisorConfig.cs` with Render URL

### Deploying Next.js App to Vercel

1. **Create Vercel Account**:
   - Go to https://vercel.com/
   - Sign up with GitHub

2. **Import Project**:
   - Click "Add New Project"
   - Import repository
   - Select `ai-advisor-web` folder

3. **Configure**:
   - **Framework Preset**: Next.js
   - **Build Command**: `npm run build`
   - **Output Directory**: `.next`

4. **Set Environment Variables**:
   - `OPENAI_API_KEY` - Your OpenAI API key
   - `NEXT_PUBLIC_API_URL` - Your Flask API URL (Render)

5. **Deploy**:
   - Click "Deploy"
   - Get your URL: `https://your-app.vercel.app`

6. **Update Config**:
   - Update `AiAdvisorConfig.ChatUrl` with Vercel URL

---

## RAG (Retrieval Augmented Generation)

### What is RAG?

**RAG** enhances AI responses by:
1. **Ingesting documents** (degree catalogs, course descriptions)
2. **Creating embeddings** (vector representations)
3. **Searching for relevant context** when user asks question
4. **Including context in prompt** to OpenAI

**Why use RAG?**
- AI has access to specific information (degree requirements)
- More accurate answers
- Can cite sources

### How It Works

1. **Ingest Documents**:
   ```typescript
   // lib/ingest.ts
   async function ingest() {
     // Fetch degree catalog pages
     const docs = await fetchDocuments();
     
     // Split into chunks
     const chunks = splitIntoChunks(docs);
     
     // Create embeddings
     const embeddings = await createEmbeddings(chunks);
     
     // Store in vector store
     loadChunks(embeddings);
   }
   ```

2. **Search for Context**:
   ```typescript
   // When user asks question
   const qEmbedding = await createEmbedding(question);
   const hits = search(qEmbedding, 6);  // Find 6 most relevant chunks
   const context = hits.map(h => h.text).join("\n\n");
   ```

3. **Include in Prompt**:
   ```typescript
   const messages = [
     { role: "system", content: SYSTEM_PROMPT },
     { 
       role: "user", 
       content: `Context:\n${context}\n\nQuestion: ${question}`
     }
   ];
   ```

---

## Testing

### Test Flask API Locally

1. **Set Environment Variable**:
   ```bash
   export OPENAI_API_KEY=your_key_here
   ```

2. **Run Flask**:
   ```bash
   cd AiAdvisorApi
   python app.py
   ```

3. **Test Endpoint**:
   ```bash
   curl -X POST http://localhost:5000/api/chat \
     -H "Content-Type: application/json" \
     -d '{"sessionId":"test","question":"What courses do I need?"}'
   ```

### Test Next.js App Locally

1. **Set Environment Variables**:
   ```bash
   export OPENAI_API_KEY=your_key_here
   export NEXT_PUBLIC_API_URL=http://localhost:5000
   ```

2. **Run Next.js**:
   ```bash
   cd ai-advisor-web
   npm run dev
   ```

3. **Open Browser**: http://localhost:3000

### Test MAUI Integration

1. **Update Config**:
   - Set `AiAdvisorConfig` URLs to localhost (for testing)

2. **Run MAUI App**:
   - Navigate to AI Advisor page
   - Verify QR code displays
   - Scan QR code with phone
   - Send message from phone
   - Verify message appears on dashboard

---

## Summary

1. **Flask API** handles chat logic and stores transcripts
2. **Next.js Web App** provides student interface
3. **MAUI App** displays conversation on dashboard
4. **RAG** enhances answers with relevant context
5. **Polling** efficiently checks for new messages

**Key Concepts**:
- **API** - Backend service
- **REST** - HTTP-based communication
- **Streaming** - Send data as it's generated
- **Polling** - Check for updates periodically
- **RAG** - Enhance AI with context

The AI Advisor provides intelligent academic guidance! 🤖
