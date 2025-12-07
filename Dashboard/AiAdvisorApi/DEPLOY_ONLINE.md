# Deploy AI Advisor API Online

## Option 1: Railway (Easiest - Recommended)

### Step 1: Create Railway Account
1. Go to [railway.app](https://railway.app)
2. Sign up with GitHub

### Step 2: Deploy
1. Click **"New Project"**
2. Select **"Deploy from GitHub repo"**
3. Select your repository
4. Railway will auto-detect it's a .NET app
5. Set the **Root Directory** to: `Dashboard/AiAdvisorApi`
6. Click **"Deploy"**

### Step 3: Get Your URL
1. After deployment, Railway gives you a URL like: `https://your-app-name.up.railway.app`
2. Copy this URL

### Step 4: Update Configs
1. **Update `Dashboard/Services/AiAdvisorConfig.cs`:**
   ```csharp
   public static string TranscriptEndpoint { get; set; } = "https://your-app-name.up.railway.app/api/transcript?sessionId=demo";
   public static string ClearTranscriptEndpoint { get; set; } = "https://your-app-name.up.railway.app/api/transcript/clear";
   ```

2. **Update Vercel Environment Variable:**
   - Go to Vercel project → Settings → Environment Variables
   - Set `NEXT_PUBLIC_API_URL` to: `https://your-app-name.up.railway.app`
   - Redeploy Vercel

3. **Rebuild MAUI app**

## Option 2: Render

### Step 1: Create Render Account
1. Go to [render.com](https://render.com)
2. Sign up with GitHub

### Step 2: Deploy
1. Click **"New +"** → **"Web Service"**
2. Connect your GitHub repo
3. Set:
   - **Name**: `ai-advisor-api`
   - **Root Directory**: `Dashboard/AiAdvisorApi`
   - **Environment**: `.NET Core`
   - **Build Command**: `dotnet publish -c Release -o ./publish`
   - **Start Command**: `dotnet ./publish/AiAdvisorApi.dll`
4. Click **"Create Web Service"**

### Step 3: Get Your URL
- Render gives you: `https://ai-advisor-api.onrender.com`

### Step 4: Update Configs (same as Railway)

## Option 3: Azure App Service

### Step 1: Create Azure Account
1. Go to [azure.com](https://azure.microsoft.com)
2. Create free account (free tier available)

### Step 2: Deploy
1. In Azure Portal, create **"Web App"**
2. Use **"Deploy from GitHub"**
3. Select your repo and `Dashboard/AiAdvisorApi` folder
4. Deploy

### Step 3: Get Your URL
- Azure gives you: `https://your-app-name.azurewebsites.net`

## After Deployment

1. **Test the API:**
   - Open: `https://your-api-url/api/transcript?sessionId=demo`
   - Should see: `[]` (empty array)

2. **Update all configs:**
   - `Dashboard/Services/AiAdvisorConfig.cs` - Use HTTPS URL
   - Vercel `NEXT_PUBLIC_API_URL` - Use HTTPS URL
   - Rebuild MAUI app

3. **Test the full flow:**
   - Open MAUI app → AI Advisor page
   - Scan QR code on phone
   - Type a question
   - Check MAUI app shows Q&A

## Important Notes

- **HTTPS is required** for Android (no more cleartext HTTP errors!)
- **Free tiers** are available on Railway and Render
- **In-memory storage** means transcripts are lost on restart (fine for demo)
- For production, consider adding a database (Redis, PostgreSQL, etc.)

## Troubleshooting

**If deployment fails:**
- Check Railway/Render logs
- Make sure `Root Directory` is set to `Dashboard/AiAdvisorApi`
- Verify `.csproj` file is correct

**If API doesn't respond:**
- Check the deployment logs
- Verify the URL is correct (HTTPS, not HTTP)
- Test the URL in browser first

