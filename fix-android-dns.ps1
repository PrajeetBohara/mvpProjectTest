# Fix Android Emulator DNS
# Run this script while your emulator is running

Write-Host "=== Android Emulator DNS Fix ===" -ForegroundColor Cyan

# Try to find ADB in common locations
$adbPaths = @(
    "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe",
    "$env:USERPROFILE\AppData\Local\Android\Sdk\platform-tools\adb.exe",
    "C:\Users\$env:USERNAME\AppData\Local\Android\Sdk\platform-tools\adb.exe",
    "$env:ANDROID_HOME\platform-tools\adb.exe"
)

$adb = $null
foreach ($path in $adbPaths) {
    if (Test-Path $path) {
        $adb = $path
        Write-Host "Found ADB at: $adb" -ForegroundColor Green
        break
    }
}

if (-not $adb) {
    Write-Host "ADB not found. Please:" -ForegroundColor Yellow
    Write-Host "1. Install Android SDK Platform Tools, OR" -ForegroundColor Yellow
    Write-Host "2. Test on a physical device instead (recommended)" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "To test on physical device:" -ForegroundColor Cyan
    Write-Host "  1. Enable USB Debugging on your phone" -ForegroundColor White
    Write-Host "  2. Connect phone via USB" -ForegroundColor White
    Write-Host "  3. In Visual Studio, select your phone as target" -ForegroundColor White
    Write-Host "  4. Run the app - it will use your WiFi DNS" -ForegroundColor White
    exit
}

# Check if emulator is connected
Write-Host "Checking for connected devices..." -ForegroundColor Cyan
$devices = & $adb devices
if ($devices -match "emulator") {
    Write-Host "Emulator detected!" -ForegroundColor Green
    
    Write-Host "Setting DNS to Google DNS (8.8.8.8, 8.8.4.4)..." -ForegroundColor Cyan
    & $adb shell "setprop net.dns1 8.8.8.8"
    & $adb shell "setprop net.dns2 8.8.4.4"
    
    Write-Host "Restarting network..." -ForegroundColor Cyan
    & $adb shell "svc wifi disable"
    Start-Sleep -Seconds 2
    & $adb shell "svc wifi enable"
    
    Write-Host ""
    Write-Host "Testing DNS resolution..." -ForegroundColor Cyan
    $result = & $adb shell "nslookup ai-advisor-api-wzez.onrender.com"
    Write-Host $result
    
    if ($result -match "Address") {
        Write-Host ""
        Write-Host "SUCCESS! DNS is working. Restart your MAUI app." -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "DNS test failed. Try:" -ForegroundColor Yellow
        Write-Host "  1. Restart the emulator" -ForegroundColor White
        Write-Host "  2. Or test on a physical device" -ForegroundColor White
    }
} else {
    Write-Host "No emulator detected. Make sure:" -ForegroundColor Yellow
    Write-Host "  1. Emulator is running" -ForegroundColor White
    Write-Host "  2. USB Debugging is enabled" -ForegroundColor White
}

