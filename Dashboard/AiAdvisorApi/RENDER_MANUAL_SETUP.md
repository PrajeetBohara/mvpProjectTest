# Manual Render Setup - Force Python

## The Problem
Render is auto-detecting .NET (because of `.csproj` file) and not showing Python option.

## Solution: Manual Override

### Step 1: Delete Current Service
1. Render dashboard → Your service
2. **Settings** → Scroll to bottom
3. **"Delete Service"** → Confirm

### Step 2: Create New Service - Manual Mode

1. Click **"New +"** → **"Web Service"**

2. **Connect Repository:**
   - Connect your GitHub repo (the one with `Dashboard` folder)

3. **When you see the setup form, look for these fields:**

   **If you see "Auto-detect" or "Environment" dropdown:**
   - Change it from "Auto-detect" to **"Python 3"**
   - Or if it says ".NET Core", change to "Python 3"

   **If you DON'T see a dropdown:**
   - Scroll down the form
   - Look for "Advanced" or "More Options" section
   - Or proceed to fill in manually (see below)

4. **Fill in these fields manually:**

   ```
   Name: ai-advisor-api
   
   Root Directory: Dashboard/AiAdvisorApi
   
   Environment: Python 3
   (If you don't see this, try "Runtime" or look in Advanced settings)
   
   Build Command: pip install -r requirements.txt
   
   Start Command: python app.py
   ```

5. **Environment Variables:**
   - Click "Add Environment Variable"
   - Key: `OPENAI_API_KEY`
   - Value: `sk-your-key-here`

6. **Click "Create Web Service"**

### Step 3: If Environment Dropdown Still Not Showing

**Try this workaround:**

1. **Temporarily rename .csproj file in GitHub:**
   - Go to your GitHub repo
   - Navigate to `Dashboard/AiAdvisorApi/`
   - Click on `AiAdvisorApi.csproj`
   - Click "Edit" (pencil icon)
   - Rename first line to: `<!-- <Project Sdk="Microsoft.NET.Sdk.Web"> -->`
   - Or rename file to `AiAdvisorApi.csproj.backup`
   - Commit the change

2. **Now create Render service:**
   - Render won't detect .NET anymore
   - Should show Python option

3. **After deployment works, you can rename it back** (won't affect Python)

### Step 4: Alternative - Use Procfile

I created a `Procfile` which Render recognizes for Python:

1. Make sure `Procfile` is in `Dashboard/AiAdvisorApi/` folder
2. Commit and push to GitHub
3. Create new service
4. Render should detect Python from `Procfile`

## What Files Render Needs to See

In `Dashboard/AiAdvisorApi/` folder:
- ✅ `app.py` (Python file - tells Render it's Python)
- ✅ `requirements.txt` (Python dependencies)
- ✅ `Procfile` (I just created this - helps Render detect Python)
- ✅ `runtime.txt` (Python version - I created this)
- ✅ `wwwroot/index.html` (web page)

**Should NOT see:**
- ❌ `Dockerfile` (deleted)
- ❌ `.csproj` (temporarily hide this if needed)

## Quick Test After Deploy

1. Open your Render URL: `https://ai-advisor-api-jzse.onrender.com`
2. Should see the chat page (not an error)
3. Check Render **Logs** - should see Python starting

## Still Can't Find Python Option?

**Last resort - Use Render Blueprint:**

1. I created `render.yaml` file
2. In Render, click **"New +"** → **"Blueprint"**
3. Connect repo
4. Render will read `render.yaml` and create Python service automatically

This bypasses the manual form entirely!

