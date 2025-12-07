# Fix Railway Docker Build Error

## The Problem
Docker can't find `AiAdvisorApi.csproj` because the build context path is wrong.

## Solution

### Step 1: Verify Root Directory in Railway

1. Go to Railway project → **Settings**
2. **Root Directory** must be: `Dashboard/AiAdvisorApi`
3. This tells Railway where to find the Dockerfile

### Step 2: Verify Files Are Committed

Make sure these files are in your GitHub repo at `Dashboard/AiAdvisorApi/`:
- ✅ `AiAdvisorApi.csproj`
- ✅ `Program.cs`
- ✅ `Dockerfile`
- ✅ `wwwroot/index.html` (and folder)

### Step 3: Check Railway Build Settings

1. Railway project → **Settings** → **Service**
2. Under **Build**:
   - **Builder**: Should be "Dockerfile" (auto-detected)
   - If not, select "Dockerfile" manually

### Step 4: Redeploy

1. Push the updated Dockerfile to GitHub
2. Railway will auto-redeploy
3. Or manually trigger: **Deployments** → **Redeploy**

## Alternative: Use Nixpacks Instead

If Docker keeps failing, switch to Nixpacks:

1. Railway → **Settings** → **Service**
2. Under **Build**, change **Builder** to "Nixpacks"
3. The `nixpacks.toml` file will be used
4. Redeploy

## Verify Build Context

The Dockerfile assumes Railway's build context is `Dashboard/AiAdvisorApi/`. 

When Railway builds:
- It sets working directory to `Dashboard/AiAdvisorApi/`
- Dockerfile should be in that directory ✅
- `AiAdvisorApi.csproj` should be in that directory ✅

If files are missing, check:
- Are they committed to GitHub?
- Is Root Directory set correctly?
- Are files in the right location?

## Updated Dockerfile

The new Dockerfile uses:
- `COPY *.csproj ./` - More flexible, finds any .csproj file
- Simplified paths
- Proper port configuration for Railway

## Still Failing?

Check Railway logs for the exact error. Common issues:
1. **File not found** → Root Directory wrong or files not committed
2. **SDK not found** → .NET 9.0 might not be available (try .NET 8.0)
3. **Build timeout** → Railway free tier has limits

If .NET 9.0 is the issue, we can downgrade to .NET 8.0.

