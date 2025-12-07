# Quick Fix: Android Emulator DNS Issue

## The Problem
Android emulator can't resolve `ai-advisor-api-wzez.onrender.com` - DNS resolution fails.

## Easiest Solution: Test on Physical Device

**This will work immediately:**

1. **Enable USB Debugging** on your Android phone:
   - Settings → About Phone → Tap "Build Number" 7 times
   - Settings → Developer Options → Enable "USB Debugging"

2. **Connect phone via USB** to your computer

3. **In Visual Studio:**
   - Change target from emulator to your phone
   - Run the app

4. **Done!** Your phone uses your WiFi's DNS (works automatically)

## Alternative: Fix Emulator DNS

### Option A: Use the PowerShell Script

1. **Run the script I created:**
   ```powershell
   .\fix-android-dns.ps1
   ```

2. **If ADB not found**, the script will tell you where to find it

### Option B: Manual ADB Commands

1. **Find ADB** (usually in):
   ```
   C:\Users\[YourName]\AppData\Local\Android\Sdk\platform-tools\adb.exe
   ```

2. **Open PowerShell** in that folder, then run:
   ```powershell
   .\adb shell "setprop net.dns1 8.8.8.8"
   .\adb shell "setprop net.dns2 8.8.4.4"
   .\adb shell "svc wifi disable"
   .\adb shell "svc wifi enable"
   ```

3. **Test:**
   ```powershell
   .\adb shell "nslookup ai-advisor-api-wzez.onrender.com"
   ```

### Option C: Add DNS to Emulator Config

1. **Find emulator config file:**
   ```
   C:\Users\[YourName]\.android\avd\[EmulatorName].avd\config.ini
   ```

2. **Add at the end:**
   ```ini
   dns.server.1=8.8.8.8
   dns.server.2=8.8.4.4
   ```

3. **Restart emulator**

## Recommended: Use Physical Device

**Why?**
- Works immediately (no DNS config needed)
- More realistic testing
- Faster than fixing emulator DNS
- Your phone's WiFi DNS already works

Just connect your phone and run the app on it instead of the emulator!

