# Fix "Connection failure" Error in MAUI App

## The Problem

If you see this error in your MAUI app logs:
```
[AiAdvisorMirrorService] Error fetching transcript: Connection failure
```

It means the MAUI app (running on Android/phone) can't reach your API because it's trying to connect to `localhost:5000`, which on Android refers to the Android device itself, not your computer.

## The Solution

**You need to replace `localhost` with your computer's IP address.**

### Step 1: Find Your Computer's IP Address

**Windows:**
1. Open PowerShell or Command Prompt
2. Type: `ipconfig`
3. Look for "IPv4 Address" under your active network adapter
4. It will look like: `192.168.1.100` or `10.0.0.5`

**Example output:**
```
Wireless LAN adapter Wi-Fi:
   IPv4 Address. . . . . . . . . . . : 192.168.1.100
```

### Step 2: Update the Config File

Open `Dashboard/Services/AiAdvisorConfig.cs` and replace `localhost` with your IP:

**Before:**
```csharp
public static string TranscriptEndpoint { get; set; } = "http://localhost:5000/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "http://localhost:5000/api/transcript/clear";
```

**After (example with IP 192.168.1.100):**
```csharp
public static string TranscriptEndpoint { get; set; } = "http://192.168.1.100:5000/api/transcript?sessionId=demo";
public static string ClearTranscriptEndpoint { get; set; } = "http://192.168.1.100:5000/api/transcript/clear";
```

### Step 3: Rebuild and Run

1. Rebuild your MAUI app
2. Make sure your API is running (`dotnet run` in `AiAdvisorApi` folder)
3. Make sure your phone/Android device and computer are on the **same Wi-Fi network**
4. Run the MAUI app again

### Step 4: Test

1. Open the AI Advisor page in your MAUI app
2. Check the logs - you should see:
   ```
   [AiAdvisorMirrorService] Fetching transcript from: http://192.168.1.100:5000/api/transcript?sessionId=demo
   [AiAdvisorMirrorService] Received X messages
   ```
3. If you still see "Connection failure", check:
   - Windows Firewall might be blocking port 5000 (see `AiAdvisorApi/NETWORK_SETUP.md`)
   - Phone and computer are on the same Wi-Fi network
   - API is actually running and listening on `0.0.0.0:5000`

## Quick Test

Test if your API is reachable from your phone:

1. On your phone's browser, go to: `http://YOUR_IP:5000/api/transcript?sessionId=demo`
2. You should see: `[]` (empty array)
3. If you see an error, the API isn't reachable (check firewall/network)

## Note for Windows Desktop

If you're running the MAUI app on Windows Desktop (same computer as the API), `localhost` works fine. You only need to change it if:
- Running on Android emulator
- Running on physical Android device
- Running on iOS device
- Running on any device different from where the API runs

