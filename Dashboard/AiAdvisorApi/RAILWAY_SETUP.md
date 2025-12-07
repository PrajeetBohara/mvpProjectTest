# Railway Deployment Setup

## Step 1: Connect Your GitHub Repository

1. Go to [railway.app](https://railway.app)
2. Sign up/Login with GitHub
3. Click **"New Project"**
4. Select **"Deploy from GitHub repo"**
5. **Select your repository** (the one that contains the `Dashboard` folder)
   - This is your main project repository
   - Railway will scan it and find the .NET project

## Step 2: Configure Root Directory

**IMPORTANT:** After connecting the repo, you MUST set the Root Directory:

1. In your Railway project, go to **Settings**
2. Scroll to **"Root Directory"** section
3. Set it to: `Dashboard/AiAdvisorApi`
4. Click **"Save"**

This tells Railway where your API project is located.

## Step 3: Deploy

1. Railway will automatically detect it's a .NET project
2. It will build and deploy automatically
3. Wait 2-3 minutes for deployment

## Step 4: Get Your URL

1. Go to **Settings** → **Networking**
2. Click **"Generate Domain"** (or Railway may auto-generate one)
3. Copy the URL (e.g., `https://ai-advisor-api-production.up.railway.app`)

## Step 5: Update Configs

**1. Update Vercel Environment Variable:**
- Go to Vercel project → Settings → Environment Variables
- Set `NEXT_PUBLIC_API_URL` = `https://your-railway-url.up.railway.app`
- **Redeploy** your Vercel project

**2. Update MAUI Config:**
- Edit `Dashboard/Services/AiAdvisorConfig.cs`
- Change to:
  ```csharp
  public static string TranscriptEndpoint { get; set; } = "https://your-railway-url.up.railway.app/api/transcript?sessionId=demo";
  public static string ClearTranscriptEndpoint { get; set; } = "https://your-railway-url.up.railway.app/api/transcript/clear";
  ```
- Rebuild your MAUI app

## Repository Structure

Your GitHub repo should look like this:
```
your-repo/
├── Dashboard/
│   ├── AiAdvisorApi/          ← Railway will deploy this
│   │   ├── Program.cs
│   │   ├── AiAdvisorApi.csproj
│   │   └── ...
│   ├── ai-advisor-web/         ← Vercel deploys this
│   └── Dashboard/              ← Your MAUI app
└── ...
```

## Troubleshooting

**If Railway can't find the project:**
- Make sure Root Directory is set to `Dashboard/AiAdvisorApi`
- Check that `AiAdvisorApi.csproj` exists in that folder
- Check Railway build logs for errors

**If deployment fails:**
- Check Railway logs (click on your service → Logs)
- Make sure .NET 9.0 is available (Railway should auto-detect)
- Verify all files are committed to GitHub

