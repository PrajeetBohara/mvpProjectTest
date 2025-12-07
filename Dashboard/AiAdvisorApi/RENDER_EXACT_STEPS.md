# Render Setup - Exact Steps (No More Failures!)

## Which Repository to Connect?

**Connect your MAIN GitHub repository** (the one that contains the `Dashboard` folder).

**Example:**
- If your repo is: `https://github.com/yourusername/your-repo-name`
- Connect that entire repo to Render

## Which Root Directory?

After connecting the repo, set **Root Directory** to:
```
Dashboard/AiAdvisorApi
```

**NOT** just `Dashboard`  
**NOT** the repo root  
**MUST BE**: `Dashboard/AiAdvisorApi`

## Step-by-Step Render Setup

### Step 1: Create New Web Service

1. Go to [render.com](https://render.com)
2. Click **"New +"** button (top right)
3. Select **"Web Service"**

### Step 2: Connect Repository

1. Click **"Connect account"** or **"Connect GitHub"**
2. Authorize Render to access your GitHub
3. Select your repository (the one with `Dashboard` folder)
4. Click **"Connect"**

### Step 3: Configure Service

Fill in these **EXACT** settings:

**Basic:**
- **Name**: `ai-advisor-api` (or any name you want)

**Build & Deploy:**
- **Environment**: **`Python 3`** ← IMPORTANT!
- **Root Directory**: **`Dashboard/AiAdvisorApi`** ← MUST BE THIS!
- **Branch**: `main` (or `master` - whatever your default branch is)
- **Build Command**: `pip install -r requirements.txt`
- **Start Command**: `python app.py`

**Environment Variables:**
- Click **"Add Environment Variable"**
- **Key**: `OPENAI_API_KEY`
- **Value**: `sk-your-openai-key-here`
- Click **"Save"**

### Step 4: Deploy

1. Click **"Create Web Service"** button at bottom
2. Wait 2-3 minutes for deployment
3. Check **"Logs"** tab to see progress

## Expected Success Logs

You should see:
```
==> Installing dependencies
Collecting flask==3.0.0
Collecting flask-cors==4.0.0
Collecting openai==1.12.0
...
Successfully installed flask flask-cors openai

==> Starting service
Starting service with 'python app.py'
Running on http://0.0.0.0:10000
```

## If Build Still Fails

Check these:

1. **Files in GitHub?**
   - Go to your GitHub repo
   - Navigate to `Dashboard/AiAdvisorApi/`
   - Verify these files exist:
     - ✅ `app.py`
     - ✅ `requirements.txt`
     - ✅ `wwwroot/index.html`
     - ❌ NO `Dockerfile` (we deleted it)

2. **Root Directory Correct?**
   - In Render → Settings
   - Root Directory must be: `Dashboard/AiAdvisorApi`
   - NOT `Dashboard/AiAdvisorApi/` (no trailing slash)
   - NOT just `Dashboard`

3. **Environment is Python?**
   - Must be **`Python 3`**
   - NOT `.NET Core`
   - NOT `Docker`

4. **Check Render Logs**
   - Click **"Logs"** tab
   - Look for error messages
   - Common errors:
     - "No such file or directory" → Root Directory wrong
     - "No module named flask" → requirements.txt not found
     - "app.py not found" → Root Directory wrong

## Repository Structure

Your GitHub repo should look like this:
```
your-repo/
├── Dashboard/
│   ├── AiAdvisorApi/          ← Render deploys from here
│   │   ├── app.py             ← Python server
│   │   ├── requirements.txt   ← Dependencies
│   │   └── wwwroot/
│   │       └── index.html     ← Web page
│   ├── Dashboard/             ← Your MAUI app
│   └── ...
└── README.md
```

## Quick Checklist

Before deploying:
- [ ] `app.py` exists in `Dashboard/AiAdvisorApi/`
- [ ] `requirements.txt` exists in `Dashboard/AiAdvisorApi/`
- [ ] `wwwroot/index.html` exists
- [ ] `Dockerfile` is DELETED
- [ ] All files committed to GitHub
- [ ] Render Root Directory = `Dashboard/AiAdvisorApi`
- [ ] Render Environment = `Python 3`
- [ ] Build Command = `pip install -r requirements.txt`
- [ ] Start Command = `python app.py`
- [ ] `OPENAI_API_KEY` environment variable set

## After Successful Deploy

You'll get a URL like: `https://ai-advisor-api-jzse.onrender.com`

Update MAUI config (already done):
- `ChatUrl` = your Render URL
- `TranscriptEndpoint` = your Render URL + `/api/transcript?sessionId=demo`
- `ClearTranscriptEndpoint` = your Render URL + `/api/transcript/clear`

Rebuild MAUI app → Done! 🎉

