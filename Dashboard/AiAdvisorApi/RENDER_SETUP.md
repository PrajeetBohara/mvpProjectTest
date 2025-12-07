# EASIEST Solution: Deploy to Render.com

## Why Render?
✅ **Easier than Railway** - Auto-detects .NET projects  
✅ **Free tier** - No credit card needed  
✅ **Simple setup** - Just connect GitHub  
✅ **HTTPS included** - Works on Android  

## Step 1: Sign Up
1. Go to [render.com](https://render.com)
2. Sign up with GitHub (free)

## Step 2: Deploy
1. Click **"New +"** → **"Web Service"**
2. Connect your GitHub repository
3. Fill in:
   - **Name**: `ai-advisor-api`
   - **Root Directory**: `Dashboard/AiAdvisorApi`
   - **Environment**: `.NET Core`
   - **Build Command**: `dotnet publish -c Release -o ./publish`
   - **Start Command**: `dotnet ./publish/AiAdvisorApi.dll`
4. Click **"Create Web Service"**

## Step 3: Add Environment Variable
1. In Render dashboard → **Environment** tab
2. Add: `OPENAI_API_KEY` = `sk-your-key-here`
3. Render auto-redeploys

## Step 4: Get Your URL
- Render gives you: `https://ai-advisor-api.onrender.com`
- Copy this URL

## Step 5: Update MAUI Config
Edit `Dashboard/Services/AiAdvisorConfig.cs`:
```csharp
public static string ChatUrl { get; set; } = "https://ai-advisor-api.onrender.com";
public static string TranscriptEndpoint { get; set; } = "https://ai-advisor-api.onrender.com/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "https://ai-advisor-api.onrender.com/api/transcript/clear";
```

## Done! 🎉
- QR code → Opens Render URL
- User types → AI responds
- MAUI shows conversation

**That's it! No Docker, no complex config, just works.**

