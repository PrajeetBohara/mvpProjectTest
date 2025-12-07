# Fix Railway Build Error

## The Problem
Railway is failing to build because it can't auto-detect the .NET project structure.

## Solution

### Option 1: Use Dockerfile (Recommended)

Railway will automatically use the `Dockerfile` if it exists. We already created one, so Railway should use it.

**Make sure:**
1. Root Directory is set to: `Dashboard/AiAdvisorApi`
2. Railway should auto-detect the Dockerfile

### Option 2: Manual Build Settings

If Dockerfile doesn't work, set these in Railway:

1. Go to Railway project → **Settings** → **Service**
2. Under **Build**, set:
   - **Build Command**: `dotnet publish -c Release -o ./publish`
   - **Start Command**: `dotnet ./publish/AiAdvisorApi.dll`

### Option 3: Check Root Directory

**IMPORTANT:** Make sure Root Directory is correct:

1. Railway project → **Settings**
2. **Root Directory** should be: `Dashboard/AiAdvisorApi`
3. NOT just `Dashboard` or empty

### Option 4: Use Render Instead (Alternative)

If Railway keeps failing, try Render:

1. Go to [render.com](https://render.com)
2. **New +** → **Web Service**
3. Connect GitHub repo
4. Set:
   - **Root Directory**: `Dashboard/AiAdvisorApi`
   - **Environment**: `.NET Core`
   - **Build Command**: `dotnet publish -c Release -o ./publish`
   - **Start Command**: `dotnet ./publish/AiAdvisorApi.dll`
5. Add environment variable: `OPENAI_API_KEY`

## Check Railway Logs

1. Click **"View logs"** in Railway
2. Look for specific error messages
3. Common issues:
   - "Could not find project file" → Root Directory is wrong
   - "SDK not found" → Need to specify .NET version
   - "Build failed" → Check if all files are committed to GitHub

## Verify Files Are Committed

Make sure these files are in your GitHub repo:
- `Dashboard/AiAdvisorApi/AiAdvisorApi.csproj`
- `Dashboard/AiAdvisorApi/Program.cs`
- `Dashboard/AiAdvisorApi/Dockerfile` (optional but recommended)
- `Dashboard/AiAdvisorApi/wwwroot/index.html`

## Quick Test

After fixing, Railway should:
1. Detect .NET project
2. Run `dotnet restore`
3. Run `dotnet publish`
4. Start the app

Check the logs to see which step fails.

