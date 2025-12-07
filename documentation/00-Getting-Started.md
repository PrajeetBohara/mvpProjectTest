# Getting Started Guide

## Welcome!

This guide will help you set up everything you need to run this Interactive Dashboard application on your device. Even if you've never coded before, we'll walk you through each step.

---

## What is This Project?

This is a **Smart TV Dashboard Application** for McNeese State University's Department of Engineering & Computer Science. It displays:
- Announcements and events
- Weather information
- Campus maps
- Student clubs information
- Faculty directory
- Academic programs
- An AI advisor for degree planning

The app is designed to run on tablets, smart TVs, or computers.

---

## Understanding the Technologies

Before we start, let's understand what each technology does:

### 1. **.NET MAUI (Multi-platform App UI)**
- **What it is**: A framework by Microsoft that lets you write one app that works on Windows, Android, iOS, and macOS
- **Why we use it**: Instead of writing separate apps for each platform, we write once and it works everywhere
- **Think of it like**: A universal translator for apps - one codebase, multiple devices

### 2. **React & Next.js**
- **What it is**: React is a JavaScript library for building user interfaces. Next.js is a framework built on top of React
- **Why we use it**: We use this for the interactive map feature - it creates a web-based map that can be embedded in the app
- **Think of it like**: Building a website that shows an interactive map

### 3. **Supabase**
- **What it is**: A cloud-based database service (like Google Sheets, but for apps)
- **Why we use it**: Stores all our data (announcements, events, faculty info, etc.) in the cloud
- **Think of it like**: A filing cabinet in the cloud that the app reads from

### 4. **Flask**
- **What it is**: A Python web framework for building APIs (Application Programming Interfaces)
- **Why we use it**: Powers the AI Advisor feature - it handles conversations between users and the AI
- **Think of it like**: A waiter in a restaurant - it takes your order (question) and brings back the answer

### 5. **Vercel**
- **What it is**: A platform for hosting web applications
- **Why we use it**: We deploy our interactive map web app here so it's accessible online
- **Think of it like**: A free parking space on the internet for our map website

### 6. **Render**
- **What it is**: Another platform for hosting web applications and APIs
- **Why we use it**: We deploy our Flask API here so the AI Advisor can work
- **Think of it like**: Another free parking space, but for our AI service

### 7. **WeatherAPI.com**
- **What it is**: A service that provides weather data
- **Why we use it**: Shows current weather in the navigation bar
- **Think of it like**: A weather station that sends us data

---

## Prerequisites (What You Need)

### For Windows Development:

1. **Visual Studio 2022** (Free Community Edition)
   - Download from: https://visualstudio.microsoft.com/
   - During installation, select:
     - ✅ .NET Multi-platform App UI development
     - ✅ .NET desktop development
     - ✅ Mobile development with .NET (for Android/iOS)

2. **.NET 9.0 SDK**
   - Usually comes with Visual Studio 2022
   - Verify installation: Open Command Prompt and type `dotnet --version`
   - Should show version 9.0.x or higher

3. **Android SDK** (if testing on Android)
   - Comes with Visual Studio 2022 when you select "Mobile development with .NET"
   - Or download Android Studio separately

4. **Node.js and npm** (for React/Next.js development)
   - Download from: https://nodejs.org/
   - Choose the LTS (Long Term Support) version
   - This installs both Node.js and npm (Node Package Manager)

5. **Python 3.8 or higher** (for Flask API)
   - Download from: https://www.python.org/downloads/
   - During installation, check "Add Python to PATH"

6. **Git** (for version control)
   - Download from: https://git-scm.com/downloads

---

## Step-by-Step Setup

### Step 1: Install Visual Studio 2022

1. Go to https://visualstudio.microsoft.com/downloads/
2. Download "Visual Studio Community 2022" (it's free)
3. Run the installer
4. Select these workloads:
   - ✅ **.NET Multi-platform App UI development**
   - ✅ **.NET desktop development**
   - ✅ **Mobile development with .NET** (optional, for Android/iOS)
5. Click "Install" and wait (this may take 30-60 minutes)

### Step 2: Verify .NET Installation

1. Open Command Prompt (Press Windows key, type "cmd", press Enter)
2. Type: `dotnet --version`
3. You should see something like: `9.0.xxx`
4. If not, reinstall Visual Studio with the correct workloads

### Step 3: Install Node.js

1. Go to https://nodejs.org/
2. Download the LTS version (recommended)
3. Run the installer
4. Check "Add to PATH" if the option appears
5. Click "Next" through all steps
6. Verify installation:
   - Open Command Prompt
   - Type: `node --version` (should show v20.x.x or similar)
   - Type: `npm --version` (should show 10.x.x or similar)

### Step 4: Install Python

1. Go to https://www.python.org/downloads/
2. Download Python 3.11 or 3.12
3. Run the installer
4. **IMPORTANT**: Check "Add Python to PATH" at the bottom
5. Click "Install Now"
6. Verify installation:
   - Open Command Prompt
   - Type: `python --version` (should show Python 3.x.x)

### Step 5: Install Git

1. Go to https://git-scm.com/downloads
2. Download for Windows
3. Run the installer
4. Use default settings (just click "Next" through all steps)

---

## Setting Up the Project

### Step 1: Open the Project

1. Open Visual Studio 2022
2. Click "Open a project or solution"
3. Navigate to the project folder
4. Select `Dashboard.sln` (the solution file)
5. Click "Open"

### Step 2: Restore NuGet Packages

NuGet is like an app store for .NET code libraries. The project needs to download dependencies.

1. In Visual Studio, go to **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Type: `dotnet restore`
3. Press Enter
4. Wait for packages to download (may take a few minutes)

**OR** simply build the project (F6) and Visual Studio will restore packages automatically.

### Step 3: Set Up Supabase (Database)

1. Go to https://supabase.com/
2. Sign up for a free account
3. Create a new project
4. Wait for the project to initialize (takes 1-2 minutes)
5. Go to **Settings** → **API**
6. Copy:
   - **Project URL** (looks like: `https://xxxxx.supabase.co`)
   - **anon/public key** (a long string starting with `eyJ...`)

7. In the project, open `Dashboard/Services/SupabaseService.cs`
8. Replace the values:
   ```csharp
   public static readonly string SupabaseUrl = "YOUR_PROJECT_URL_HERE";
   public static readonly string SupabaseAnonKey = "YOUR_ANON_KEY_HERE";
   ```

9. Set up the database tables:
   - In Supabase, go to **SQL Editor**
   - Open `Backend/supabase_schema.sql` from the project
   - Copy and paste the SQL code into the SQL Editor
   - Click "Run"
   - This creates all the tables needed

10. Set up Storage:
    - In Supabase, go to **Storage**
    - Create buckets:
      - `images` (make it public)
      - `videos` (make it public)
      - `documents` (keep it private)

### Step 4: Set Up Weather API

1. Go to https://www.weatherapi.com/
2. Sign up for a free account
3. Go to your dashboard
4. Copy your API key
5. In the project, open `Dashboard/Services/WeatherService.cs`
6. Find this line (around line 21):
   ```csharp
   _apiKey = "f3ea5473f30a4e14b0c35729251808"; // Replace with your key
   ```
7. Replace the API key with your own

### Step 5: Set Up AI Advisor (Flask API)

1. Open Command Prompt
2. Navigate to the project folder: `cd "path\to\Dashboard\AiAdvisorApi"`
3. Install Flask:
   ```bash
   pip install flask flask-cors openai
   ```
4. Set up environment variable:
   - Create a file named `.env` in the `AiAdvisorApi` folder
   - Add: `OPENAI_API_KEY=your_openai_api_key_here`
   - Get an OpenAI API key from https://platform.openai.com/api-keys

5. Test locally:
   ```bash
   python app.py
   ```
   - Should see: "Running on http://0.0.0.0:5000"

### Step 6: Set Up Interactive Map (React/Next.js)

1. Open Command Prompt
2. Navigate to: `cd "path\to\Dashboard\ai-advisor-web"`
3. Install dependencies:
   ```bash
   npm install
   ```
   - This downloads all required packages (may take 2-5 minutes)

4. Test locally:
   ```bash
   npm run dev
   ```
   - Should see: "Ready on http://localhost:3000"

---

## Running the Application

### Running the .NET MAUI App

1. Open `Dashboard.sln` in Visual Studio 2022
2. At the top, you'll see a dropdown for target platform:
   - Select **Windows Machine** (for Windows)
   - Select **Android Emulator** (for Android testing)
   - Or connect a physical device
3. Press **F5** or click the green "Start" button
4. The app will build and launch

**First time building may take 5-10 minutes** - this is normal!

### Running the AI Advisor Web Interface

1. Open Command Prompt
2. Navigate to `Dashboard/ai-advisor-web`
3. Run: `npm run dev`
4. Open browser to: http://localhost:3000

### Running the Flask API

1. Open Command Prompt
2. Navigate to `Dashboard/AiAdvisorApi`
3. Run: `python app.py`
4. API will be available at: http://localhost:5000

---

## Common Issues and Solutions

### Issue: "dotnet command not found"
**Solution**: Reinstall Visual Studio 2022 with .NET workload selected

### Issue: "npm command not found"
**Solution**: Reinstall Node.js and make sure to check "Add to PATH"

### Issue: "python command not found"
**Solution**: Reinstall Python and check "Add Python to PATH"

### Issue: Build errors about missing packages
**Solution**: 
1. Right-click the solution in Visual Studio
2. Select "Restore NuGet Packages"
3. Wait for completion
4. Rebuild (Ctrl+Shift+B)

### Issue: Android emulator not starting
**Solution**:
1. Open Android SDK Manager in Visual Studio
2. Install Android SDK Platform 33 or higher
3. Create a new Android Virtual Device (AVD)

### Issue: Can't connect to Supabase
**Solution**:
1. Check your internet connection
2. Verify Supabase URL and key in `SupabaseService.cs`
3. Check Supabase dashboard to ensure project is active

---

## Next Steps

Now that you have everything set up:
1. Read `01-Project-Structure.md` to understand the codebase
2. Read `02-NET-MAUI-App.md` to understand the main app
3. Read `07-Building-and-Deployment.md` to learn how to create an .exe file

---

## Getting Help

If you encounter issues:
1. Check the error message carefully
2. Search for the error online
3. Check the project's README.md
4. Review the documentation files in this folder

---

## Summary

You've learned:
- ✅ What each technology does
- ✅ How to install all required software
- ✅ How to set up the project
- ✅ How to run the application
- ✅ How to troubleshoot common issues

**Congratulations!** You're ready to start working with the codebase. 🎉
