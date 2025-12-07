# Supabase Integration - Complete Guide

This document explains how Supabase works and how it's integrated into this project, written for beginners.

---

## What is Supabase?

**Supabase** is a cloud-based database and backend service. Think of it as:
- **Google Sheets in the cloud** - but much more powerful
- **A filing cabinet** that your app can read from and write to
- **A server** that stores all your data (announcements, events, faculty, etc.)

**What it provides**:
- **PostgreSQL Database** - Stores structured data (like Excel, but better)
- **Storage** - Stores files (images, videos, documents)
- **Authentication** - User login system (not used in this project)
- **Real-time** - Live updates (not used in this project)

---

## Why Do We Use Supabase?

Instead of storing data in the app itself, we store it in Supabase so that:
1. **Data can be updated** without updating the app
2. **Multiple devices** can access the same data
3. **Non-developers** can update content (through Supabase dashboard)
4. **Scalable** - Can handle lots of data and users

**Example**: When a new event is added, we just add it to Supabase - the app automatically shows it!

---

## How It Works - The Big Picture

```
App needs data (e.g., faculty list)
    ↓
App calls FacultyService
    ↓
FacultyService builds API URL
    ↓
FacultyService makes HTTP request to Supabase
    ↓
Supabase queries database
    ↓
Supabase returns JSON data
    ↓
FacultyService parses JSON
    ↓
FacultyService returns C# objects
    ↓
App displays data on screen
```

---

## Setting Up Supabase

### Step 1: Create Account and Project

1. Go to https://supabase.com/
2. Click "Start your project"
3. Sign up (free account)
4. Click "New Project"
5. Fill in:
   - **Name**: "Dashboard Project" (or any name)
   - **Database Password**: Create a strong password (save it!)
   - **Region**: Choose closest to you
6. Click "Create new project"
7. Wait 1-2 minutes for project to initialize

### Step 2: Get API Credentials

1. In Supabase dashboard, go to **Settings** → **API**
2. Copy these values:
   - **Project URL**: `https://xxxxx.supabase.co`
   - **anon/public key**: Long string starting with `eyJ...`

### Step 3: Configure in Code

**File**: `Dashboard/Services/SupabaseService.cs`

```csharp
public static class SupabaseService
{
    public static readonly string SupabaseUrl = "https://YOUR_PROJECT.supabase.co";
    public static readonly string SupabaseAnonKey = "YOUR_ANON_KEY_HERE";
}
```

**Replace**:
- `YOUR_PROJECT` with your project ID
- `YOUR_ANON_KEY_HERE` with your anon key

**Why these are public?**
- The anon key has limited permissions (read-only for most tables)
- It's safe to include in the app
- For write operations, you'd use a service role key (server-side only)

---

## Database Structure

### Understanding Tables

A **table** is like a spreadsheet with columns and rows.

**Example: Faculty Table**

| id | name | email | department | office | photo_url |
|----|------|-------|------------|--------|-----------|
| 1 | Dr. Smith | smith@mcneese.edu | CS | Drew 101 | https://... |
| 2 | Dr. Jones | jones@mcneese.edu | Engineering | Drew 102 | https://... |

**Columns** (fields):
- `id` - Unique identifier
- `name` - Faculty name
- `email` - Email address
- `department` - Department name
- `office` - Office location
- `photo_url` - Link to photo

**Rows** (records):
- Each row is one faculty member

### Our Database Tables

Created by running `Backend/supabase_schema.sql`:

1. **announcements** - Department announcements
2. **clubs** - Student organizations
3. **events** - Important dates and events
4. **faculty** - Faculty directory
5. **homepage_images** - Slideshow images
6. **gallery** - Photo gallery
7. **projects** - Senior design projects
8. **sponsors** - Sponsors and donors
9. **courses** - Course information
10. **profiles** - User profiles (if needed)

---

## Setting Up the Database

### Step 1: Run the Schema Script

1. In Supabase dashboard, go to **SQL Editor**
2. Open `Backend/supabase_schema.sql` from the project
3. Copy the entire SQL script
4. Paste into SQL Editor
5. Click "Run" (or press Ctrl+Enter)
6. Wait for "Success" message

**What this does**:
- Creates all tables
- Sets up columns with correct data types
- Creates relationships between tables
- Sets up Row Level Security (RLS)

### Step 2: Add Sample Data

1. In SQL Editor, open `supabase_sample_data.sql`
2. Copy and paste into SQL Editor
3. Click "Run"
4. This adds example data for testing

**Or** add data manually through the dashboard:
1. Go to **Table Editor**
2. Select a table (e.g., "faculty")
3. Click "Insert row"
4. Fill in the fields
5. Click "Save"

---

## How Services Connect to Supabase

### Example: FacultyService

**File**: `Dashboard/Services/FacultyService.cs`

```csharp
public class FacultyService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public FacultyService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;  // Get from config
        _supabaseKey = SupabaseService.SupabaseAnonKey;  // Get from config

        // Add authentication headers
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    public async Task<List<Faculty>> GetAllFacultyAsync()
    {
        // Build the API URL
        var url = $"{_supabaseUrl}/rest/v1/faculty?select=*";
        
        // Make the request
        var response = await _httpClient.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            // Read JSON response
            var json = await response.Content.ReadAsStringAsync();
            
            // Parse JSON into C# objects
            var faculty = JsonSerializer.Deserialize<List<Faculty>>(json);
            return faculty ?? new List<Faculty>();
        }
        
        return new List<Faculty>();  // Return empty on error
    }
}
```

**Breaking it down**:

1. **Get Credentials**:
   ```csharp
   _supabaseUrl = SupabaseService.SupabaseUrl;
   _supabaseKey = SupabaseService.SupabaseAnonKey;
   ```
   - Gets URL and key from the config class

2. **Set Headers**:
   ```csharp
   _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
   _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
   ```
   - Supabase requires these headers for authentication
   - `apikey` - Identifies your project
   - `Authorization` - Proves you have permission

3. **Build URL**:
   ```csharp
   var url = $"{_supabaseUrl}/rest/v1/faculty?select=*";
   ```
   - `rest/v1/` - Supabase REST API endpoint
   - `faculty` - Table name
   - `select=*` - Get all columns

4. **Make Request**:
   ```csharp
   var response = await _httpClient.GetAsync(url);
   ```
   - Sends GET request to Supabase
   - Waits for response

5. **Parse Response**:
   ```csharp
   var json = await response.Content.ReadAsStringAsync();
   var faculty = JsonSerializer.Deserialize<List<Faculty>>(json);
   ```
   - Reads JSON string
   - Converts to C# `Faculty` objects

---

## Supabase REST API

Supabase provides a REST API that follows a pattern:

### Base URL
```
https://YOUR_PROJECT.supabase.co/rest/v1/
```

### Endpoints

**Get all records**:
```
GET /rest/v1/faculty?select=*
```

**Get filtered records**:
```
GET /rest/v1/faculty?department=eq.CS
```
- `department=eq.CS` means "where department equals CS"

**Get limited records**:
```
GET /rest/v1/faculty?select=*&limit=10
```

**Order by**:
```
GET /rest/v1/faculty?select=*&order=name.asc
```

**Combine filters**:
```
GET /rest/v1/faculty?department=eq.CS&office=like.*Drew*&order=name.asc
```

### Query Parameters

- `select=*` - Select all columns (or specify: `select=name,email`)
- `limit=10` - Limit to 10 records
- `offset=20` - Skip first 20 records (pagination)
- `order=name.asc` - Sort by name ascending
- `column=eq.value` - Filter: column equals value
- `column=like.*text*` - Filter: column contains text
- `column=gt.100` - Filter: column greater than 100

---

## Example: HomePageImageService

**File**: `Dashboard/Services/HomePageImageService.cs`

This service fetches homepage slideshow images:

```csharp
public async Task<List<HomePageImage>> GetFeaturedHomePageImagesAsync(int limit = 5)
{
    // Build URL with filters
    var url = $"{_supabaseUrl}/rest/v1/homepage_images?" +
              $"is_active=eq.true&" +  // Only active images
              $"order=display_order.asc&" +  // Order by display order
              $"limit={limit}";  // Limit results

    var httpResponse = await _httpClient.GetAsync(url);

    if (httpResponse.IsSuccessStatusCode)
    {
        var response = await httpResponse.Content.ReadAsStringAsync();
        var images = JsonSerializer.Deserialize<List<HomePageImage>>(response);
        return images ?? new List<HomePageImage>();
    }

    // Fallback to hardcoded URLs if database fails
    return GetDirectImageUrls();
}
```

**What this does**:
1. Builds URL with filters:
   - Only get active images (`is_active=eq.true`)
   - Order by display order (`order=display_order.asc`)
   - Limit to 5 images (`limit=5`)

2. Makes request to Supabase

3. Parses response into `HomePageImage` objects

4. Falls back to hardcoded URLs if database fails (error handling)

---

## Storage (File Uploads)

Supabase also provides file storage (like Google Drive).

### Setting Up Storage

1. In Supabase dashboard, go to **Storage**
2. Click "Create bucket"
3. Create buckets:
   - **images** - Make it public
   - **videos** - Make it public
   - **documents** - Keep it private

### Storage Policies

**Public Read Access** (for images):
```sql
CREATE POLICY "Public read access"
ON storage.objects FOR SELECT
USING (bucket_id = 'images'::text AND auth.role() = 'anon'::text);
```

**What this means**:
- Anyone can read files in the `images` bucket
- No authentication required
- Perfect for public images

### Using Storage URLs

When you upload an image, Supabase gives you a URL:
```
https://YOUR_PROJECT.supabase.co/storage/v1/object/public/images/homepage/image1.jpg
```

**Structure**:
- `storage/v1/object/public` - Public storage endpoint
- `images` - Bucket name
- `homepage/image1.jpg` - File path

**In code**:
```csharp
public class HomePageImage
{
    public string ImageUrl { get; set; }  
    // Example: "https://xxx.supabase.co/storage/v1/object/public/images/homepage/img.jpg"
}
```

---

## Row Level Security (RLS)

**What is RLS?**
- Security feature that controls who can read/write data
- Each table has policies

**Our Setup**:
- Most tables: **Public read access** (anyone can read)
- Write operations: **Authenticated users only** (not used in this app)

**Example Policy**:
```sql
CREATE POLICY "Public read access"
ON faculty FOR SELECT
USING (true);  -- Anyone can read
```

**Why this is safe**:
- App only reads data (doesn't write)
- No sensitive data exposed
- Public data (faculty, events) should be public

---

## Data Models

Each table has a corresponding C# model:

### Faculty Model

**Database Table**: `faculty`

```sql
CREATE TABLE faculty (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    email TEXT,
    department TEXT,
    office TEXT,
    photo_url TEXT
);
```

**C# Model**: `Dashboard/Models/Faculty.cs`

```csharp
public class Faculty
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}
```

**Mapping**:
- SQL `TEXT` → C# `string`
- SQL `UUID` → C# `Guid`
- Column names match property names (or use `[JsonPropertyName]`)

---

## Error Handling

All services include error handling:

```csharp
try
{
    var response = await _httpClient.GetAsync(url);
    
    if (response.IsSuccessStatusCode)
    {
        // Parse and return data
    }
    else
    {
        // Log error
        System.Diagnostics.Debug.WriteLine($"Error: {response.StatusCode}");
        return new List<Faculty>();  // Return empty list
    }
}
catch (HttpRequestException ex)
{
    // Network error
    System.Diagnostics.Debug.WriteLine($"Network error: {ex.Message}");
    return new List<Faculty>();
}
catch (Exception ex)
{
    // Any other error
    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
    return new List<Faculty>();
}
```

**What can go wrong**:
- No internet connection
- Invalid API key
- Table doesn't exist
- Network timeout

**What we do**:
- Return empty list (app doesn't crash)
- Log error for debugging
- Show fallback data if available

---

## Testing Supabase Connection

### Test in Code

```csharp
var service = new FacultyService();
var faculty = await service.GetAllFacultyAsync();

if (faculty.Count > 0)
{
    Console.WriteLine($"Found {faculty.Count} faculty members");
    foreach (var f in faculty)
    {
        Console.WriteLine($"- {f.Name} ({f.Department})");
    }
}
else
{
    Console.WriteLine("No faculty found or connection failed");
}
```

### Test in Browser

Open this URL (replace YOUR_PROJECT and YOUR_KEY):
```
https://YOUR_PROJECT.supabase.co/rest/v1/faculty?select=*&apikey=YOUR_KEY
```

You should see JSON with faculty data.

### Test in Supabase Dashboard

1. Go to **Table Editor**
2. Select "faculty" table
3. You should see all records
4. You can add/edit/delete records here

---

## Adding New Data

### Through Supabase Dashboard

1. Go to **Table Editor**
2. Select a table
3. Click "Insert row"
4. Fill in fields
5. Click "Save"
6. App will show new data on next refresh

### Through SQL

1. Go to **SQL Editor**
2. Write INSERT statement:
   ```sql
   INSERT INTO faculty (id, name, email, department, office)
   VALUES (gen_random_uuid(), 'Dr. New', 'new@mcneese.edu', 'CS', 'Drew 103');
   ```
3. Click "Run"

---

## Best Practices

1. **Always use try-catch** around Supabase calls
2. **Return empty collections** on error (don't return null)
3. **Log errors** for debugging
4. **Use filters** to limit data (don't fetch everything)
5. **Cache data** when possible (reduce API calls)
6. **Handle network errors** gracefully

---

## Summary

1. **Supabase** stores all app data in the cloud
2. **Services** make HTTP requests to Supabase REST API
3. **JSON responses** are parsed into C# objects
4. **App displays** data on screen
5. **Data can be updated** in Supabase without updating the app

**Key Concepts**:
- **Table** - Like a spreadsheet
- **REST API** - Way to access data over HTTP
- **JSON** - Data format
- **RLS** - Security policies
- **Storage** - File storage

The Supabase integration is now fully explained! 🗄️
