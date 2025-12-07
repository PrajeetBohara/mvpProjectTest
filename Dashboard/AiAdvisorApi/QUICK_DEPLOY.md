# Quick Deploy to Railway (5 minutes)

## Step 1: Push to GitHub
Make sure your code is pushed to GitHub.

## Step 2: Deploy on Railway
1. Go to [railway.app](https://railway.app) → Sign up with GitHub
2. Click **"New Project"** → **"Deploy from GitHub repo"**
3. Select your repo
4. In project settings, set **Root Directory** to: `Dashboard/AiAdvisorApi`
5. Click **"Deploy"** (takes 2-3 minutes)

## Step 3: Get Your URL
1. After deployment, go to **Settings** → **Networking**
2. Click **"Generate Domain"** (or use the default)
3. Copy the URL (e.g., `https://ai-advisor-api-production.up.railway.app`)

## Step 4: Update Configs

**1. Update `Dashboard/Services/AiAdvisorConfig.cs`:**
```csharp
public static string TranscriptEndpoint { get; set; } = "https://YOUR-RAILWAY-URL/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "https://YOUR-RAILWAY-URL/api/transcript/clear";
```

**2. Update Vercel:**
- Go to Vercel project → Settings → Environment Variables
- Set `NEXT_PUBLIC_API_URL` = `https://YOUR-RAILWAY-URL`
- Redeploy Vercel

**3. Rebuild MAUI app**

## Step 5: Test
1. Open: `https://YOUR-RAILWAY-URL/api/transcript?sessionId=demo` in browser
2. Should see: `[]`
3. Run MAUI app → AI Advisor page → Should work!

## Done! 🎉

No more local network issues, no more HTTP cleartext errors. Everything works online!

