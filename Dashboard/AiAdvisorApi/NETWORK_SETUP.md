# Network Setup for AI Advisor API

## The Problem

When you access the React web page on your phone, it tries to connect to `http://localhost:5000`. But `localhost` on your phone refers to the phone itself, not your computer where the API is running!

## Solution: Use Your Computer's IP Address

### Step 1: Find Your Computer's IP Address

**Windows:**
1. Open Command Prompt or PowerShell
2. Type: `ipconfig`
3. Look for "IPv4 Address" under your active network adapter (usually "Wireless LAN adapter Wi-Fi" or "Ethernet adapter")
4. It will look like: `192.168.1.100` or `10.0.0.5`

**Example output:**
```
Wireless LAN adapter Wi-Fi:
   IPv4 Address. . . . . . . . . . . : 192.168.1.100
```

### Step 2: Update Vercel Environment Variable

1. Go to your Vercel project dashboard
2. Go to **Settings** → **Environment Variables**
3. Find or create: `NEXT_PUBLIC_API_URL`
4. Set it to: `http://YOUR_IP_ADDRESS:5000`
   - Example: `http://192.168.1.100:5000`
5. **Important**: Make sure your phone and computer are on the **same Wi-Fi network**
6. Click **Save**
7. **Redeploy** your Vercel project

### Step 3: Update MAUI Config (if needed)

If your MAUI app is running on the same computer, `localhost:5000` should work fine.

If your MAUI app is running on a different device (like a Smart TV), update `Dashboard/Services/AiAdvisorConfig.cs`:

```csharp
public static string TranscriptEndpoint { get; set; } = "http://YOUR_IP_ADDRESS:5000/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "http://YOUR_IP_ADDRESS:5000/api/transcript/clear";
```

### Step 4: Allow Firewall Access (Windows)

Windows Firewall might block incoming connections. To allow it:

1. Open **Windows Defender Firewall**
2. Click **Advanced settings**
3. Click **Inbound Rules** → **New Rule**
4. Select **Port** → **Next**
5. Select **TCP** and enter port **5000** → **Next**
6. Select **Allow the connection** → **Next**
7. Check all profiles → **Next**
8. Name it "AI Advisor API" → **Finish**

Or use PowerShell (as Administrator):
```powershell
New-NetFirewallRule -DisplayName "AI Advisor API" -Direction Inbound -LocalPort 5000 -Protocol TCP -Action Allow
```

### Step 5: Test

1. Make sure your API is running: `dotnet run` in `AiAdvisorApi` folder
2. On your phone, open the Vercel web page
3. Type a question and click "Ask"
4. Check the API terminal - you should see log messages like:
   ```
   [API] Received POST /api/chat for session: demo
   [API] Added user message. Total messages: 1
   [API] Added assistant message. Total messages: 2
   ```
5. Check the MAUI app - messages should appear in the Live Transcript section

## Troubleshooting

**If you see errors in the browser console:**
- Open browser DevTools (F12) → Console tab
- Look for error messages about network requests
- Common errors:
  - `Failed to fetch` = Can't reach the API (check IP address, firewall, same network)
  - `CORS error` = API CORS is configured, but check if API is running

**If API terminal shows no requests:**
- The React page isn't reaching your API
- Check that `NEXT_PUBLIC_API_URL` is set correctly in Vercel
- Make sure phone and computer are on same Wi-Fi
- Check Windows Firewall

**If MAUI shows nothing:**
- Check the debug output in Visual Studio
- Look for `[AiAdvisorMirrorService]` messages
- Verify `TranscriptEndpoint` in `AiAdvisorConfig.cs` is correct

## Quick Test

Test if your API is reachable from your phone:

1. On your phone's browser, go to: `http://YOUR_IP_ADDRESS:5000/api/transcript?sessionId=demo`
2. You should see: `[]` (empty array)
3. If you see an error, the API isn't reachable (check firewall/network)

