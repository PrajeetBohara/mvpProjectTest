# Fix Android Emulator Network Issue

## The Problem
Android emulator cannot resolve DNS for "ai-advisor-api-wzez.onrender.com"

## Solutions

### Solution 1: Fix Emulator DNS (Recommended)

1. **Open Android Studio**
2. **Go to AVD Manager** (Tools → Device Manager)
3. **Click the pencil icon** (Edit) next to your emulator
4. **Click "Show Advanced Settings"**
5. **Under "Network"**, set:
   - **DNS 1**: `8.8.8.8` (Google DNS)
   - **DNS 2**: `8.8.4.4` (Google DNS backup)
6. **Click "Finish"**
7. **Cold Boot** the emulator (click the dropdown arrow → "Cold Boot Now")

### Solution 2: Use Host Machine's DNS

1. **Find your computer's DNS servers:**
   - Windows: Open Command Prompt → `ipconfig /all`
   - Look for "DNS Servers" under your network adapter
   
2. **Set emulator DNS to your computer's DNS** (same steps as Solution 1)

### Solution 3: Test on Physical Device

If emulator networking is too problematic:
1. **Enable USB Debugging** on your Android phone
2. **Connect phone via USB**
3. **Run the app on your phone** instead of emulator
4. Phone will use your WiFi's DNS (should work fine)

### Solution 4: Use IP Address (Temporary Test)

If DNS is the issue, you can temporarily test with the IP address:

1. **Find Render.com's IP:**
   ```bash
   nslookup ai-advisor-api-wzez.onrender.com
   ```

2. **Update `AiAdvisorConfig.cs`** to use IP address temporarily:
   ```csharp
   public static string TranscriptEndpoint { get; set; } = "https://[IP_ADDRESS]/api/transcript?sessionId=demo";
   ```

   **Note:** This won't work with HTTPS (certificate mismatch), so only for testing HTTP if you enable cleartext.

## Verify Network Access

After fixing DNS, test in emulator:

1. **Open Browser in emulator**
2. **Navigate to:** `https://ai-advisor-api-wzez.onrender.com`
3. **Should load the AI Advisor page**

If it loads, the emulator can reach the API and the app should work.

## Quick Test Command

In Android Studio's Terminal (or ADB shell):
```bash
adb shell ping -c 3 8.8.8.8
```

If ping works, internet is fine. Then test DNS:
```bash
adb shell nslookup ai-advisor-api-wzez.onrender.com
```

If nslookup fails, DNS is the problem → Use Solution 1.

