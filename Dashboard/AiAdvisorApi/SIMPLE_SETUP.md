# Simple Setup - One Deployment, No Vercel!

## What This Does

✅ QR code in MAUI app → Opens web page on phone  
✅ User types question → API calls OpenAI  
✅ Conversation shows in MAUI app  
✅ **Everything in one Railway deployment - no Vercel needed!**

## Step 1: Deploy to Railway

1. Go to [railway.app](https://railway.app) → Sign up with GitHub
2. **New Project** → **Deploy from GitHub repo**
3. Select your repo
4. Set **Root Directory** to: `Dashboard/AiAdvisorApi`
5. Click **Deploy**

## Step 2: Set Environment Variable

1. In Railway project → **Variables** tab
2. Add: `OPENAI_API_KEY` = `sk-your-openai-key-here`
3. Railway will auto-redeploy

## Step 3: Get Your URL

1. Railway → **Settings** → **Networking**
2. Click **Generate Domain**
3. Copy URL (e.g., `https://ai-advisor-api.up.railway.app`)

## Step 4: Update MAUI Config

Edit `Dashboard/Services/AiAdvisorConfig.cs`:

```csharp
public static string ChatUrl { get; set; } = "https://your-railway-url.up.railway.app";
public static string TranscriptEndpoint { get; set; } = "https://your-railway-url.up.railway.app/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "https://your-railway-url.up.railway.app/api/transcript/clear";
```

Rebuild MAUI app.

## Step 5: Test

1. Run MAUI app → Go to AI Advisor page
2. Scan QR code with phone
3. Web page opens: `https://your-railway-url.up.railway.app`
4. Type a question → Get AI answer
5. Check MAUI app → Conversation appears!

## That's It! 🎉

- **One URL** for everything
- **No Vercel** needed
- **No local network** issues
- **HTTPS** (works on Android)
- **Simple HTML** page (no React complexity)

## How It Works

1. **Railway serves:**
   - `/` → HTML chat page (wwwroot/index.html)
   - `/api/chat` → Handles questions, calls OpenAI, stores Q&A
   - `/api/transcript` → Returns conversation for MAUI app

2. **User flow:**
   - Scan QR → Opens Railway URL
   - Type question → API calls OpenAI
   - MAUI polls `/api/transcript` → Shows conversation

## Troubleshooting

**If OpenAI doesn't work:**
- Check Railway Variables → `OPENAI_API_KEY` is set
- Check Railway logs for errors

**If web page doesn't load:**
- Make sure `wwwroot/index.html` exists
- Check Railway logs

**If MAUI doesn't show messages:**
- Check `AiAdvisorConfig.cs` has correct Railway URL
- Check Railway logs to see if `/api/transcript` is being called

