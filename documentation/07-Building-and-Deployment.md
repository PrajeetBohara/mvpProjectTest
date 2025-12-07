# Building and Deployment Guide

This comprehensive guide explains how to build the application for different platforms, change resolutions, and deploy to various environments.

---

## Table of Contents

1. [Building for Windows (.exe)](#building-for-windows-exe)
2. [Building for Android](#building-for-android)
3. [Building for iOS](#building-for-ios)
4. [Changing Screen Resolutions](#changing-screen-resolutions)
5. [Platform-Specific Adjustments](#platform-specific-adjustments)
6. [Deployment Options](#deployment-options)

---

## Building for Windows (.exe)

### Prerequisites

- Visual Studio 2022 with .NET MAUI workload
- Windows 10/11 SDK
- .NET 9.0 SDK

### Step 1: Configure Project

1. Open `Dashboard.sln` in Visual Studio 2022
2. Right-click the `Dashboard` project → **Properties**
3. Go to **Application** tab:
   - **Application Title**: "ENCS Dashboard"
   - **Application Display Version**: "1.0"
   - **Application Version**: "1"

### Step 2: Configure Windows-Specific Settings

1. In project properties, go to **Windows** tab
2. Set:
   - **Target Version**: Windows 10, version 19041 or higher
   - **Min Version**: Windows 10, version 17763 or higher

### Step 3: Build Configuration

1. In Visual Studio, select **Release** configuration (not Debug)
2. Select target: **Windows Machine** (or specific Windows version)

### Step 4: Publish as .exe

#### Method 1: Using Visual Studio

1. Right-click `Dashboard` project → **Publish**
2. Click **New**
3. Select **Folder** as publish target
4. Choose output folder (e.g., `bin\Release\net9.0-windows10.0.19041.0\publish`)
5. Click **Finish**
6. Click **Publish**

#### Method 2: Using Command Line

```powershell
# Navigate to project folder
cd Dashboard

# Publish for Windows
dotnet publish -f net9.0-windows10.0.19041.0 -c Release -p:PublishSingleFile=true -p:SelfContained=true
```

**Output location**: `bin\Release\net9.0-windows10.0.19041.0\publish\Dashboard.exe`

### Step 5: Single-File Executable

The project is already configured for single-file publishing:

```xml
<!-- In Dashboard.csproj -->
<PublishSingleFile Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">true</PublishSingleFile>
<SelfContained Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">true</SelfContained>
```

**What this means**:
- **PublishSingleFile**: Creates one .exe file (no separate DLLs)
- **SelfContained**: Includes .NET runtime (no need to install .NET separately)

### Step 6: Test the .exe

1. Navigate to publish folder
2. Double-click `Dashboard.exe`
3. Verify app runs correctly

### Step 7: Create Installer (Optional)

#### Using WiX Toolset

1. Install WiX Toolset: https://wixtoolset.org/
2. Create `.wxs` file for installer
3. Build installer package

#### Using Inno Setup

1. Download Inno Setup: https://jrsoftware.org/isinfo.php
2. Create installer script
3. Build installer

---

## Building for Android

### Prerequisites

- Visual Studio 2022 with Android workload
- Android SDK (comes with Visual Studio)
- Java Development Kit (JDK)

### Step 1: Configure Android Settings

1. Right-click `Dashboard` project → **Properties**
2. Go to **Android Manifest**:
   - **Package Name**: `com.mcneese.dashboard`
   - **Version Name**: "1.0"
   - **Version Code**: "1"
   - **Minimum Android Version**: API 21 (Android 5.0)
   - **Target Android Version**: API 33 or higher

### Step 2: Set Up Signing

1. Go to **Android Package Signing**
2. Create a new keystore:
   - Click **Create New Keystore**
   - Set password
   - Fill in certificate information
   - Save keystore file

**For testing**: Use debug keystore (auto-generated)

### Step 3: Build APK

#### Method 1: Using Visual Studio

1. Select **Release** configuration
2. Select target: **Android** → **Any CPU** (or specific device)
3. Right-click project → **Archive**
4. Click **Archive All**
5. Select archive → **Distribute**
6. Choose **Ad Hoc** (for testing) or **Google Play** (for store)
7. Click **Save As** → Save as APK

#### Method 2: Using Command Line

```bash
# Navigate to project folder
cd Dashboard

# Build APK
dotnet build -f net9.0-android -c Release

# Or publish
dotnet publish -f net9.0-android -c Release
```

**Output location**: `bin\Release\net9.0-android\publish\Dashboard-Signed.apk`

### Step 4: Install on Device

1. Enable **Developer Options** on Android device:
   - Go to Settings → About Phone
   - Tap "Build Number" 7 times
2. Enable **USB Debugging**:
   - Settings → Developer Options → USB Debugging
3. Connect device via USB
4. Install APK:
   ```bash
   adb install Dashboard-Signed.apk
   ```

---

## Building for iOS

### Prerequisites

- **macOS** (required - iOS builds only work on Mac)
- Xcode (latest version)
- Apple Developer Account ($99/year)
- Visual Studio for Mac OR Visual Studio 2022 on Windows (with Mac build host)

### Step 1: Configure iOS Settings

1. Right-click `Dashboard` project → **Properties**
2. Go to **iOS Bundle Signing**:
   - **Bundle Identifier**: `com.mcneese.dashboard`
   - **Version**: "1.0"
   - **Build**: "1"
   - **Minimum iOS Version**: 11.0

### Step 2: Set Up Provisioning Profile

1. In Xcode, go to **Signing & Capabilities**
2. Select your development team
3. Xcode automatically creates provisioning profile

### Step 3: Build IPA

#### Using Visual Studio for Mac

1. Select **Release** configuration
2. Select target: **iOS** → **iPhone** (or iPad)
3. Right-click project → **Archive**
4. Click **Archive All**
5. Select archive → **Distribute**
6. Choose distribution method (App Store, Ad Hoc, Enterprise)
7. Export IPA file

#### Using Command Line (on Mac)

```bash
# Navigate to project folder
cd Dashboard

# Build for iOS
dotnet build -f net9.0-ios -c Release

# Or publish
dotnet publish -f net9.0-ios -c Release
```

### Step 4: Install on Device

1. Connect iPhone/iPad via USB
2. In Xcode, go to **Window** → **Devices and Simulators**
3. Select your device
4. Drag IPA file to "Installed Apps"

---

## Changing Screen Resolutions

### Understanding Screen Sizes

Different devices have different screen sizes:
- **Tablets**: 10-12 inches (1920x1080, 2560x1600)
- **Smart TVs**: 32-65 inches (3840x2160 - 4K)
- **Phones**: 5-7 inches (1080x1920, 1440x2560)

### Method 1: XAML Layout Adjustments

Use responsive layouts that adapt to screen size:

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />  <!-- Adapts to content -->
        <RowDefinition Height="*" />      <!-- Takes remaining space -->
    </Grid.RowDefinitions>
    
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />    <!-- Flexible width -->
        <ColumnDefinition Width="Auto" /> <!-- Adapts to content -->
    </Grid.ColumnDefinitions>
</Grid>
```

**Key concepts**:
- `Auto` - Size based on content
- `*` - Proportional sizing (takes available space)
- `Absolute` - Fixed size (e.g., `100`)

### Method 2: Device-Specific Resources

Create platform-specific resource dictionaries:

**Resources/Values/Styles.xaml** (default):
```xml
<ResourceDictionary>
    <x:Double x:Key="HeaderFontSize">24</x:Double>
    <x:Double x:Key="BodyFontSize">16</x:Double>
</ResourceDictionary>
```

**Resources/Values/Styles.Tablet.xaml** (for tablets):
```xml
<ResourceDictionary>
    <x:Double x:Key="HeaderFontSize">32</x:Double>
    <x:Double x:Key="BodyFontSize">20</x:Double>
</ResourceDictionary>
```

**Resources/Values/Styles.TV.xaml** (for TVs):
```xml
<ResourceDictionary>
    <x:Double x:Key="HeaderFontSize">48</x:Double>
    <x:Double x:Key="BodyFontSize">28</x:Double>
</ResourceDictionary>
```

### Method 3: Code-Based Adjustments

Detect screen size and adjust programmatically:

```csharp
public partial class HomePage : ContentPage
{
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        // Adjust based on screen size
        if (width > 1920)  // Large screen (TV)
        {
            HeaderLabel.FontSize = 48;
            BodyLabel.FontSize = 28;
        }
        else if (width > 1280)  // Tablet
        {
            HeaderLabel.FontSize = 32;
            BodyLabel.FontSize = 20;
        }
        else  // Phone
        {
            HeaderLabel.FontSize = 24;
            BodyLabel.FontSize = 16;
        }
    }
}
```

### Method 4: Platform-Specific Adjustments

Adjust for specific platforms:

```csharp
#if WINDOWS
    // Windows-specific sizing
    MainGrid.Padding = new Thickness(20);
#elif ANDROID
    // Android-specific sizing
    MainGrid.Padding = new Thickness(10);
#elif IOS
    // iOS-specific sizing
    MainGrid.Padding = new Thickness(15);
#endif
```

### Method 5: Using Converters

Create value converters for dynamic sizing:

```csharp
public class ScreenSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            // Calculate font size based on screen width
            return width / 40;  // Example: 1920px → 48pt font
        }
        return 16;  // Default
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

**Usage in XAML**:
```xml
<Label FontSize="{Binding Source={x:Reference Page}, Path=Width, Converter={StaticResource ScreenSizeConverter}}" />
```

---

## Platform-Specific Adjustments

### Windows

**File**: `Platforms/Windows/App.xaml`

```xml
<Application>
    <Application.Resources>
        <ResourceDictionary>
            <!-- Windows-specific styles -->
            <Style TargetType="Button">
                <Setter Property="FontSize" Value="16" />
                <Setter Property="Padding" Value="12,8" />
            </Style>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### Android

**File**: `Platforms/Android/MainActivity.cs`

```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);
    
    // Android-specific initialization
    Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#003087"));
}
```

**File**: `Platforms/Android/AndroidManifest.xml`

```xml
<manifest>
    <uses-feature android:name="android.hardware.touchscreen" android:required="false" />
    <supports-screens
        android:smallScreens="true"
        android:normalScreens="true"
        android:largeScreens="true"
        android:xlargeScreens="true" />
</manifest>
```

### iOS

**File**: `Platforms/iOS/Info.plist`

```xml
<key>UISupportedInterfaceOrientations</key>
<array>
    <string>UIInterfaceOrientationLandscapeLeft</string>
    <string>UIInterfaceOrientationLandscapeRight</string>
    <string>UIInterfaceOrientationPortrait</string>
</array>
```

---

## Deployment Options

### Option 1: Microsoft Store (Windows)

1. **Create Developer Account**:
   - Go to https://partner.microsoft.com/dashboard
   - Sign up ($19 one-time fee)

2. **Create App Package**:
   - In Visual Studio: **Project** → **Publish** → **Create App Packages**
   - Select **Microsoft Store**
   - Follow wizard

3. **Submit to Store**:
   - Upload package to Microsoft Partner Center
   - Fill in app details
   - Submit for review

### Option 2: Google Play Store (Android)

1. **Create Developer Account**:
   - Go to https://play.google.com/console
   - Sign up ($25 one-time fee)

2. **Create App Bundle**:
   - Build AAB (Android App Bundle) instead of APK
   - In Visual Studio: **Archive** → **Distribute** → **Google Play**

3. **Upload to Play Console**:
   - Create app listing
   - Upload AAB
   - Fill in store listing
   - Submit for review

### Option 3: Apple App Store (iOS)

1. **Create Developer Account**:
   - Go to https://developer.apple.com/
   - Enroll ($99/year)

2. **Create App in App Store Connect**:
   - Go to https://appstoreconnect.apple.com/
   - Create new app
   - Fill in details

3. **Archive and Upload**:
   - Archive in Xcode
   - Upload to App Store Connect
   - Submit for review

### Option 4: Direct Distribution

#### Windows
- Distribute .exe file directly
- Users run installer
- No store required

#### Android
- Distribute APK file
- Users enable "Install from Unknown Sources"
- Install APK directly

#### iOS
- Use TestFlight for beta testing
- Or Enterprise distribution (requires Enterprise account)

---

## Testing on Different Resolutions

### Windows

1. **Change Display Resolution**:
   - Right-click desktop → **Display Settings**
   - Change resolution
   - Test app at different resolutions

2. **Use Virtual Machines**:
   - Create VMs with different resolutions
   - Test app in each

### Android

1. **Android Emulator**:
   - Create emulators with different screen sizes
   - Test app on each

2. **Physical Devices**:
   - Test on phones, tablets, TV boxes

### iOS

1. **iOS Simulator**:
   - Test on different iPhone/iPad models
   - Different screen sizes

2. **Physical Devices**:
   - Test on actual devices

---

## Performance Optimization

### For Large Screens (TVs)

1. **Increase Image Sizes**:
   - Use higher resolution images
   - Optimize for 4K displays

2. **Adjust Font Sizes**:
   - Larger fonts for readability
   - More spacing

3. **Optimize Animations**:
   - Smooth 60fps animations
   - Reduce complexity

### For Small Screens (Phones)

1. **Reduce Image Sizes**:
   - Lower resolution images
   - Compress assets

2. **Compact Layouts**:
   - Stack elements vertically
   - Use tabs/navigation

3. **Touch Targets**:
   - Minimum 44x44 points
   - Adequate spacing

---

## Summary

1. **Windows**: Build .exe with `dotnet publish`
2. **Android**: Build APK/AAB, sign, distribute
3. **iOS**: Build on Mac, archive, upload to App Store
4. **Resolutions**: Use responsive layouts, device-specific resources
5. **Deployment**: Choose store or direct distribution

**Key Commands**:
- `dotnet publish` - Build for deployment
- `dotnet build` - Build for testing
- Archive (Visual Studio) - Create distributable packages

You're now ready to build and deploy the app! 🚀
