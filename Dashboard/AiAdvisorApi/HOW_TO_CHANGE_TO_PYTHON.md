# How to Change Render to Python (Step by Step)

## The Problem
Render is detecting .NET (because of `.csproj` file) and showing Docker options instead of Python.

## Solution: Delete the Service and Recreate

Render locks the environment type when you first create the service. You need to **delete and recreate** it.

### Step 1: Delete Current Service

1. Go to Render dashboard
2. Click on your service: `ai-advisor-api-jzse`
3. Go to **Settings** tab
4. Scroll to bottom
5. Click **"Delete Service"** or **"Suspend Service"**
6. Confirm deletion

### Step 2: Create New Service (Python)

1. Click **"New +"** → **"Web Service"**
2. Connect your GitHub repository (same one)
3. **IMPORTANT:** When you see the setup screen, look for:

   **"Environment"** dropdown - This is where you pick Python!
   
   If you don't see it, it might be auto-detecting. Try:
   - Scroll down on the setup page
   - Look for "Environment" or "Runtime" dropdown
   - Select **"Python 3"** or **"Python"**

4. Fill in:
   - **Name**: `ai-advisor-api`
   - **Root Directory**: `Dashboard/AiAdvisorApi`
   - **Environment**: **`Python 3`** ← SELECT THIS!
   - **Build Command**: `pip install -r requirements.txt`
   - **Start Command**: `python app.py`

5. Click **"Create Web Service"**

### Step 3: If You Still Don't See Python Option

**Option A: Use render.yaml (Automatic)**

I created `render.yaml` file. Render should auto-detect it:
1. Make sure `render.yaml` is in `Dashboard/AiAdvisorApi/` folder
2. Commit and push to GitHub
3. Create new service - Render should detect Python from `render.yaml`

**Option B: Manual Override**

1. Create service as "Blank Web Service"
2. Connect repo
3. Manually set all settings:
   - Environment: Python 3
   - Root Directory: Dashboard/AiAdvisorApi
   - Build: `pip install -r requirements.txt`
   - Start: `python app.py`

## Alternative: Hide .NET Files from Render

If Render keeps detecting .NET, we can temporarily move/rename the .csproj file:

1. Rename `AiAdvisorApi.csproj` to `AiAdvisorApi.csproj.backup`
2. Commit and push
3. Create new Render service - should detect Python now
4. (You can rename it back later if needed)

## Quick Test

After creating the service, check the **Logs** tab. You should see:
```
Installing dependencies from requirements.txt
Collecting flask==3.0.0
...
Starting service with 'python app.py'
```

If you see `.NET` or `Docker` in the logs, it's still using the wrong environment.

## Still Stuck?

Share a screenshot of the Render "Create Web Service" page. I'll tell you exactly where to click!

