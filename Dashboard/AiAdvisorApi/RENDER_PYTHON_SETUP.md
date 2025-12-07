# Render Python Setup - Step by Step

## Step 1: Delete/Rename Dockerfile (Important!)

Render will try to use Dockerfile if it exists. We need Python, not Docker.

**Option A:** Delete `Dockerfile` from `Dashboard/AiAdvisorApi/` folder
**Option B:** Rename it to `Dockerfile.backup`

## Step 2: Render Settings

Go to your Render service → **Settings** tab:

### Basic Settings:
- **Name**: `ai-advisor-api` (or whatever you named it)
- **Root Directory**: `Dashboard/AiAdvisorApi` ✅

### Build & Deploy:
- **Environment**: Change to **`Python 3`** (NOT .NET Core!)
- **Build Command**: `pip install -r requirements.txt`
- **Start Command**: `python app.py`

### Environment Variables:
- **Key**: `OPENAI_API_KEY`
- **Value**: `sk-your-openai-key-here`

## Step 3: Save and Deploy

1. Click **"Save Changes"**
2. Render will auto-redeploy
3. Wait 2-3 minutes
4. Check **Logs** tab to see if it's working

## Step 4: Verify Files Are Committed

Make sure these files are in GitHub at `Dashboard/AiAdvisorApi/`:
- ✅ `app.py`
- ✅ `requirements.txt`
- ✅ `wwwroot/index.html`
- ❌ NO `Dockerfile` (delete or rename it!)

## Expected Logs

You should see:
```
Starting service with 'python app.py'
Running on http://0.0.0.0:10000
```

## If It Still Fails

Check Render logs for errors. Common issues:
- "No module named flask" → requirements.txt not found
- "app.py not found" → Root Directory wrong
- "Port already in use" → Should use PORT env var (Render sets this automatically)

