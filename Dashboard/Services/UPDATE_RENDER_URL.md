# How to Add Render URL to MAUI Config

## Step 1: Get Your Render URL

After deploying to Render, you'll get a URL like:
- `https://ai-advisor-api.onrender.com`
- Or `https://ai-advisor-api-xxxx.onrender.com`

Copy this URL.

## Step 2: Update MAUI Config

Open: `Dashboard/Services/AiAdvisorConfig.cs`

Replace `YOUR-RENDER-URL` with your actual Render URL in **3 places**:

### Example (if your Render URL is `https://ai-advisor-api.onrender.com`):

```csharp
public static string ChatUrl { get; set; } = "https://ai-advisor-api.onrender.com";

public static string TranscriptEndpoint { get; set; } = "https://ai-advisor-api.onrender.com/api/transcript?sessionId=demo";

public static string ClearTranscriptEndpoint { get; set; } = "https://ai-advisor-api.onrender.com/api/transcript/clear";
```

## Step 3: Rebuild MAUI App

After updating the config:
1. Rebuild your MAUI project
2. Run the app
3. Go to AI Advisor page
4. Scan QR code → Should open your Render URL!

## Quick Checklist

- [ ] Deployed Python Flask app to Render
- [ ] Got Render URL (e.g., `https://xxx.onrender.com`)
- [ ] Updated `ChatUrl` in `AiAdvisorConfig.cs`
- [ ] Updated `TranscriptEndpoint` in `AiAdvisorConfig.cs`
- [ ] Updated `ClearTranscriptEndpoint` in `AiAdvisorConfig.cs`
- [ ] Rebuilt MAUI app
- [ ] Tested QR code scan

## That's It! 🎉

Your Render URL goes in all 3 places in `AiAdvisorConfig.cs`.

