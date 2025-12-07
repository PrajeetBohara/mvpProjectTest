# AI Advisor Setup Guide - Step by Step

This guide explains how to set up and run the AI Advisor system with the simple API.

## Overview

The system has 3 parts:
1. **Simple API** - Stores Q&A in memory (runs locally or on a server)
2. **Web Page** (Vercel) - User types questions, gets AI answers, sends Q&A to API
3. **MAUI App** - Displays the conversation on the Smart TV/display

---

## Step 1: Run the Simple API

### Option A: Run Locally (for testing)

1. Open a terminal/command prompt
2. Navigate to the API folder:
   ```bash
   cd "E:\Senior Design Fall 2025 Project Data\Proxy\Dashboard\Dashboard\AiAdvisorApi"
   ```
3. Run the API:
   ```bash
   dotnet run
   ```
4. You should see output like:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5000
   ```
5. **Keep this terminal open** - the API must stay running!

### Option B: Deploy to Production (later)

You can deploy this API to:
- **Azure App Service** (free tier available)
- **Railway** (railway.app) - very easy
- **Render** (render.com) - free tier
- **Your own server**

For now, use **Option A** (local) for testing.

---

## Step 2: Configure the Web Page (Vercel)

The web page needs to know where your API is running.

### If API is running locally (`http://localhost:5000`):

1. Go to your Vercel project dashboard
2. Go to **Settings** → **Environment Variables**
3. Add a new variable:
   - **Name**: `NEXT_PUBLIC_API_URL`
   - **Value**: `http://localhost:5000` (for local testing)
   - **Environments**: Select **Production**, **Preview**, **Development**
   - Click **Save**
4. **Redeploy** your Vercel project (go to **Deployments** → click **Redeploy**)

### If API is deployed (e.g., `https://your-api.railway.app`):

1. Same steps as above, but use your deployed API URL instead
2. Example: `NEXT_PUBLIC_API_URL=https://your-api.railway.app`

**Important**: The web page will POST Q&A to this API after getting answers from OpenAI.

---

## Step 3: Configure the MAUI App

The MAUI app needs to know where your API is to poll for transcripts.

1. Open `Dashboard/Services/AiAdvisorConfig.cs`
2. Update these lines:

```csharp
// If API is running locally:
public static string TranscriptEndpoint { get; set; } = "http://localhost:5000/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "http://localhost:5000/api/transcript/clear";

// If API is deployed (example):
// public static string TranscriptEndpoint { get; set; } = "https://your-api.railway.app/api/transcript?sessionId=demo";
// public static string ClearTranscriptEndpoint { get; set; } = "https://your-api.railway.app/api/transcript/clear";
```

3. **Rebuild** your MAUI app:
   ```bash
   cd "E:\Senior Design Fall 2025 Project Data\Proxy\Dashboard\Dashboard"
   dotnet build
   ```

---

## Step 4: Test the System

### Test 1: Check API is running

1. Open a browser
2. Go to: `http://localhost:5000/api/transcript?sessionId=demo`
3. You should see: `[]` (empty array - no messages yet)

### Test 2: Test the full flow

1. **Start the API** (Step 1) - keep terminal open
2. **Open MAUI app** - go to "AI Advisor" page
3. **Scan QR code** with your phone - opens the Vercel web page
4. **Type a question** in the web page (e.g., "What courses do I need for CS?")
5. **Click "Ask"** - web page gets answer from OpenAI
6. **Check MAUI app** - within 2-5 seconds, you should see:
   - Your question (as "user")
   - The AI answer (as "assistant")

### Troubleshooting

**If MAUI shows nothing:**
- Check API is running: `http://localhost:5000/api/transcript?sessionId=demo`
- Check `AiAdvisorConfig.cs` has correct API URL
- Check MAUI app console for errors

**If web page doesn't send to API:**
- Check Vercel env var `NEXT_PUBLIC_API_URL` is set
- Check Vercel logs for errors
- Make sure API is accessible from internet (if not localhost)

---

## Step 5: Deploy API for Production

### Option: Railway (Easiest)

1. Go to [railway.app](https://railway.app) and sign up
2. Click **New Project** → **Deploy from GitHub repo**
3. Select your repo
4. Railway will auto-detect it's a .NET app
5. After deploy, Railway gives you a URL like: `https://your-app.railway.app`
6. Update `AiAdvisorConfig.cs` and Vercel env var with this URL

### Option: Azure App Service

1. Create an Azure account (free tier available)
2. Create a new "Web App"
3. Deploy the `AiAdvisorApi` folder
4. Get the URL and update configs

---

## How It Works

```
User types question on phone (Vercel web page)
    ↓
Web page calls OpenAI (via Vercel /api/chat)
    ↓
Gets AI answer
    ↓
Web page POSTs both Q&A to Simple API (/api/chat)
    ↓
Simple API stores in memory
    ↓
MAUI app polls Simple API every 2 seconds (/api/transcript)
    ↓
MAUI displays Q&A on Smart TV
```

---

## Important Notes

1. **API must stay running** - if you close the terminal, transcripts stop working
2. **For production**, deploy the API to a server (Railway, Azure, etc.)
3. **Session ID** - both web and MAUI use `"demo"` by default
4. **CORS** - API allows all origins (for development; restrict in production)

---

## Quick Start Checklist

- [ ] Run API: `cd Dashboard/AiAdvisorApi && dotnet run`
- [ ] Set Vercel env var: `NEXT_PUBLIC_API_URL=http://localhost:5000`
- [ ] Update `AiAdvisorConfig.cs` with API URL
- [ ] Rebuild MAUI: `dotnet build`
- [ ] Test: Scan QR → Type question → Check MAUI shows Q&A

---

## Need Help?

- Check API is running: Visit `http://localhost:5000/api/transcript?sessionId=demo`
- Check MAUI config: `Dashboard/Services/AiAdvisorConfig.cs`
- Check Vercel logs: Project → Deployments → Click latest → View logs

