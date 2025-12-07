# Fix Android Emulator DNS via ADB (Easiest Method)

## Method 1: Set DNS via ADB (While Emulator is Running)

1. **Make sure your emulator is running**

2. **Open Command Prompt or PowerShell** (in your project directory)

3. **Run these commands:**

```powershell
# Set DNS to Google DNS
adb shell "settings put global private_dns_mode off"
adb shell "setprop net.dns1 8.8.8.8"
adb shell "setprop net.dns2 8.8.4.4"
```

4. **Restart network on emulator:**
```powershell
adb shell "svc wifi disable"
adb shell "svc wifi enable"
```

5. **Test if DNS works:**
```powershell
adb shell ping -c 3 8.8.8.8
adb shell nslookup ai-advisor-api-wzez.onrender.com
```

If `nslookup` returns an IP address, DNS is working!

## Method 2: Add DNS to Emulator Startup (Permanent)

1. **Find your emulator's config.ini file:**
   - Usually in: `C:\Users\[YourUsername]\.android\avd\[EmulatorName].avd\config.ini`

2. **Open `config.ini` in a text editor**

3. **Add these lines at the end:**
```ini
dns.server.1=8.8.8.8
dns.server.2=8.8.4.4
```

4. **Save and restart emulator**

## Method 3: Use Emulator Command Line (When Starting)

Instead of starting emulator from Android Studio:

1. **Find your emulator's name:**
   ```powershell
   emulator -list-avds
   ```

2. **Start emulator with DNS:**
   ```powershell
   emulator -avd [YourEmulatorName] -dns-server 8.8.8.8,8.8.4.4
   ```

## Method 4: Test on Physical Device (Fastest)

1. **Enable USB Debugging** on your Android phone
2. **Connect phone via USB**
3. **In Visual Studio**, change target from emulator to your phone
4. **Run the app** - phone uses your WiFi DNS (should work immediately)

## Quick Test Script

Save this as `fix-dns.ps1` and run it:

```powershell
Write-Host "Setting DNS on Android emulator..."
adb shell "setprop net.dns1 8.8.8.8"
adb shell "setprop net.dns2 8.8.4.4"
Write-Host "DNS set to 8.8.8.8 and 8.8.4.4"
Write-Host "Testing DNS..."
adb shell "nslookup ai-advisor-api-wzez.onrender.com"
```

