# Render Settings - Exact Steps

## Go to Render Dashboard

1. Open [render.com](https://render.com)
2. Click on your service: `ai-advisor-api-jzse`

## Settings Tab

Click **"Settings"** tab at the top.

## Change These Settings:

### 1. Environment
- Find **"Environment"** dropdown
- Change from: `.NET Core` 
- Change to: **`Python 3`** ✅

### 2. Build Command
- Find **"Build Command"** field
- Enter: `pip install -r requirements.txt`

### 3. Start Command  
- Find **"Start Command"** field
- Enter: `python app.py`

### 4. Root Directory (should already be correct)
- Should be: `Dashboard/AiAdvisorApi`
- If not, change it to this

### 5. Environment Variables
- Click **"Environment"** section
- Add/Edit: `OPENAI_API_KEY` = `sk-your-key`

## Save

Click **"Save Changes"** button at bottom.

## Redeploy

Render will automatically redeploy. Wait 2-3 minutes.

## Check Logs

Click **"Logs"** tab. You should see:
```
Installing dependencies...
Starting service with 'python app.py'
Running on http://0.0.0.0:10000
```

## Important: Delete Dockerfile!

Before deploying, make sure `Dockerfile` is deleted from `Dashboard/AiAdvisorApi/` folder in your GitHub repo. Render will try to use it if it exists!

