# Quick Fix: Switch Render to Python

## The Problem
Render is trying to build .NET (Dockerfile) but failing. We need Python instead.

## Solution (2 minutes)

### Step 1: Delete Dockerfile
In your GitHub repo, delete or rename:
- `Dashboard/AiAdvisorApi/Dockerfile` → Delete it or rename to `Dockerfile.backup`

**Why?** Render will use Dockerfile if it exists. We want Python, not Docker.

### Step 2: Update Render Settings

Go to Render dashboard → Your service → **Settings** tab:

**Change these:**

1. **Environment**: 
   - Change from `.NET Core` to **`Python 3`**

2. **Build Command**:
   ```
   pip install -r requirements.txt
   ```

3. **Start Command**:
   ```
   python app.py
   ```

4. **Root Directory** (should already be):
   ```
   Dashboard/AiAdvisorApi
   ```

5. **Environment Variables**:
   - `OPENAI_API_KEY` = `sk-your-key-here`

### Step 3: Save and Redeploy

1. Click **"Save Changes"**
2. Render auto-redeploys
3. Check **Logs** tab - should see Python starting

## Verify Files in GitHub

Make sure these exist in `Dashboard/AiAdvisorApi/`:
- ✅ `app.py` (Python Flask server)
- ✅ `requirements.txt` (dependencies)
- ✅ `wwwroot/index.html` (web page)
- ❌ NO `Dockerfile` (delete it!)

## Expected Success

Render logs should show:
```
Installing dependencies from requirements.txt...
Starting service with 'python app.py'
Running on http://0.0.0.0:10000
```

## QR Code Issue

The QR code should appear automatically. If it doesn't:
1. Rebuild your MAUI app (to pick up the config change)
2. Check if `ChatUrl` in config is correct: `https://ai-advisor-api-jzse.onrender.com`
3. The QR code is generated from that URL

If still not showing, check MAUI app logs for errors.

