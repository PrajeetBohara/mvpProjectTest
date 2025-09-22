Tasks Done: JAEL RUIZ (Backend)
We can invite clients as the developer in our Supabase backend incase they want to see the backend tables and policies

Taks done:
1. Created new project.

2. Ran Schema Script (scripts are in "supabase_schema.sql" file)

3. Verified Tables

4. Added Sample Data for testing

5. Following tables are made so far
   - announcements
   - clubs
   - courses
   - events
   - faculty
   - gallery
   - profiles
   - projects
   - sponsors

6. Set Up File Storage
Create Storage Buckets for images, videos and documents
images and videos are public but documents are private for now

7. Set Storage Policies
Image Policy:
Policy Name: `Public read access`
Policy Definition:
   sql code
   (bucket_id = 'images'::text) AND (auth.role() = 'anon'::text)

Video Policy:
Policy Name: `Public read access`
Policy Definition:
   sql code
   (bucket_id = 'videos'::text) AND (auth.role() = 'anon'::text)
Operation: `SELECT`

8.Stored API keys

9.Tested the Setup

10.Tested Database Connection

11. Tested Authentication

12. Configured Security

13. Reviewed Row Level Security
- All tables have RLS enabled
- Public read access for most data
- Only authenticated users can modify profiles
