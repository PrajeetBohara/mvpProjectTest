# 🚀 Supabase Setup Guide for Dashboard Backend

## Step 1: Create Supabase Account & Project

### 1.1 Sign Up
1. Go to [https://supabase.com](https://supabase.com)
2. Click **"Start your project"**
3. Sign up with GitHub, Google, or email
4. Verify your email if required

### 1.2 Create New Project
1. Click **"New Project"**
2. **Organization**: Create new organization (e.g., "McNeese University")
3. **Project Name**: `Dashboard-Backend`
4. **Database Password**: Create a strong password (SAVE THIS!)
5. **Region**: Choose closest to your location (e.g., "US East (N. Virginia)")
6. **Pricing Plan**: Select **"Free"** (perfect for development)

### 1.3 Wait for Setup
- Project creation takes 2-3 minutes
- You'll see a progress indicator

## Step 2: Set Up Database Schema

### 2.1 Access SQL Editor
1. In your Supabase dashboard, click **"SQL Editor"** in the left sidebar
2. Click **"New Query"**

### 2.2 Run Schema Script
1. Copy the entire contents of `supabase_schema.sql`
2. Paste it into the SQL Editor
3. Click **"Run"** (or press Ctrl+Enter)
4. Wait for all tables to be created (should take 10-15 seconds)

### 2.3 Verify Tables
1. Go to **"Table Editor"** in the left sidebar
2. You should see these tables:
   - profiles
   - faculty
   - clubs
   - events
   - announcements
   - projects
   - gallery
   - courses
   - sponsors

## Step 3: Add Sample Data

### 3.1 Run Sample Data Script
1. Go back to **"SQL Editor"**
2. Copy the entire contents of `supabase_sample_data.sql`
3. Paste it into a new query
4. Click **"Run"**

### 3.2 Verify Data
1. Go to **"Table Editor"**
2. Click on each table to see the sample data
3. You should see:
   - 5 profiles (1 admin, 2 faculty, 2 students)
   - 2 faculty members
   - 2 clubs
   - 3 events
   - 3 announcements
   - 3 projects
   - 3 gallery items
   - 5 courses
   - 4 sponsors

## Step 4: Set Up File Storage

### 4.1 Create Storage Buckets
1. Go to **"Storage"** in the left sidebar
2. Click **"Create a new bucket"**
3. Create these buckets:
   - **Name**: `images` | **Public**: ✅ Yes
   - **Name**: `documents` | **Public**: ❌ No
   - **Name**: `videos` | **Public**: ✅ Yes

### 4.2 Set Storage Policies
1. Click on the **"images"** bucket
2. Go to **"Policies"** tab
3. Click **"New Policy"**
4. **Policy Name**: `Public read access`
5. **Policy Definition**:
   ```sql
   (bucket_id = 'images'::text) AND (auth.role() = 'anon'::text)
   ```
6. **Operation**: `SELECT`
7. Click **"Save"**
8. Repeat for **"videos"** bucket

## Step 5: Get API Keys

### 5.1 Access Project Settings
1. Click the **gear icon** (Settings) in the left sidebar
2. Click **"API"**

### 5.2 Copy Important Information
Save these values (you'll need them later):
- **Project URL**: `https://your-project-id.supabase.co`
- **anon public key**: `eyJ...` (starts with eyJ)
- **service_role key**: `eyJ...` (starts with eyJ) - KEEP SECRET!

## Step 6: Test Your Setup

### 6.1 Test Database Connection
1. Go to **"API"** → **"REST"**
2. Try this URL in your browser:
   ```
   https://your-project-id.supabase.co/rest/v1/events?select=*
   ```
3. You should see JSON data with your events

### 6.2 Test Authentication
1. Go to **"Authentication"** → **"Users"**
2. Click **"Add user"**
3. Create a test user with email and password
4. Verify the user appears in the **"profiles"** table

## Step 7: Configure Security

### 7.1 Review Row Level Security
- All tables have RLS enabled
- Public read access for most data
- Only authenticated users can modify profiles

### 7.2 Set Up Authentication (Optional)
1. Go to **"Authentication"** → **"Settings"**
2. Configure email templates
3. Set up social providers if needed

## 🎉 You're Done!

Your Supabase backend is now ready with:
- ✅ Complete database schema
- ✅ Sample data for testing
- ✅ File storage for images/videos
- ✅ Row Level Security policies
- ✅ API endpoints ready to use

## Next Steps

1. **Test the API** using the REST endpoints
2. **Set up your MAUI app** to connect to Supabase
3. **Add more data** through the Supabase dashboard
4. **Customize** the schema for your specific needs

## 📚 Useful Resources

- [Supabase Documentation](https://supabase.com/docs)
- [Supabase JavaScript Client](https://supabase.com/docs/reference/javascript)
- [Row Level Security Guide](https://supabase.com/docs/guides/auth/row-level-security)

## 🔧 Troubleshooting

### Common Issues:
1. **"Permission denied"** - Check RLS policies
2. **"Invalid API key"** - Verify you're using the correct key
3. **"Table doesn't exist"** - Make sure you ran the schema script
4. **"CORS error"** - This is normal for now, will be fixed when connecting to MAUI

### Getting Help:
- Check the Supabase documentation
- Join the Supabase Discord community
- Ask questions in the Supabase GitHub discussions
