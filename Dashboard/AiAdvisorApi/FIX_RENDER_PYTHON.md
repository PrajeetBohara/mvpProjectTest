# Fix: Render Not Showing Python Option

## The Problem
Render is detecting `.csproj` file and thinking it's a .NET project, so it shows Docker/.NET options instead of Python.

## Solution 1: Delete and Recreate Service (Easiest)

### Step 1: Delete Current Service
1. Render dashboard → Your service
2. **Settings** tab → Scroll to bottom
3. Click **"Delete Service"**
4. Confirm

### Step 2: Create New Service
1. Click **"New +"** → **"Web Service"**
2. Connect GitHub repo
3. **Look for "Environment" or "Runtime" dropdown**
   - It might be hidden or auto-detected
   - If you see "Auto-detect", change it to **"Python 3"**
   - Or look for a dropdown that says ".NET Core" and change it to "Python 3"

4. Fill in:
   - **Name**: `ai-advisor-api`
   - **Root Directory**: `Dashboard/AiAdvisorApi`
   - **Environment/Runtime**: **`Python 3`**
   - **Build Command**: `pip install -r requirements.txt`
   - **Start Command**: `python app.py`

## Solution 2: Use render.yaml (Automatic)

I created `render.yaml` file. Render should auto-detect Python from it:

1. **Commit and push** `render.yaml` to GitHub:
   ```bash
   git add Dashboard/AiAdvisorApi/render.yaml
   git commit -m "Add render.yaml for Python"
   git push
   ```

2. **Delete current service** in Render

3. **Create new service** → Connect repo
   - Render should now detect Python from `render.yaml`
   - It should auto-fill the settings

## Solution 3: Temporarily Hide .NET Files

If Render still detects .NET, temporarily rename the .csproj file:

1. In GitHub, rename: `AiAdvisorApi.csproj` → `AiAdvisorApi.csproj.backup`
2. Commit and push
3. Create new Render service → Should detect Python now
4. (You can rename it back later - it won't affect Python deployment)

## Where to Find "Environment" Option

When creating a new service, the "Environment" dropdown might be:
- At the top of the form
- In a "Advanced" or "More Options" section
- Auto-detected (but you can override it)
- Called "Runtime" instead of "Environment"

**Look for any dropdown that says:**
- "Auto-detect" → Change to "Python 3"
- ".NET Core" → Change to "Python 3"
- "Docker" → Change to "Python 3"

## If You Still Can't Find It

**Try this:**
1. Create service as **"Blank Web Service"** (not auto-detect)
2. Then manually configure everything
3. This gives you full control

## After Creating

Check the **Logs** tab. You should see:
```
Installing dependencies...
Collecting flask==3.0.0
...
Starting service with 'python app.py'
```

If you see `.NET` or `Docker` in logs, it's still wrong.

## Quick Fix Right Now

**Easiest way:**
1. Delete current Render service
2. Commit `render.yaml` to GitHub (I just created it)
3. Create new service → Render should auto-detect Python from `render.yaml`
4. Done!

