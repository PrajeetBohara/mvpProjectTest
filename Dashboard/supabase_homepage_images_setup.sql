-- ============================================
-- Supabase Setup Script for Homepage Images
-- ============================================
-- Run this script in your Supabase SQL Editor
-- ============================================

-- Step 1: Create the homepage_images table
CREATE TABLE IF NOT EXISTS homepage_images (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title TEXT NOT NULL,
    description TEXT,
    image_url TEXT NOT NULL,
    thumbnail_url TEXT,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Step 2: Create indexes for better performance
CREATE INDEX IF NOT EXISTS idx_homepage_images_display_order ON homepage_images(display_order);
CREATE INDEX IF NOT EXISTS idx_homepage_images_is_active ON homepage_images(is_active);

-- Step 3: Enable Row Level Security
ALTER TABLE homepage_images ENABLE ROW LEVEL SECURITY;

-- Step 4: Create policy for public read access (required for app to fetch images)
CREATE POLICY "Allow public read access" ON homepage_images
    FOR SELECT
    USING (true);

-- Step 5: (Optional) Insert sample data
-- Replace the image URLs with your actual image URLs from Supabase Storage
-- 
-- Example with Supabase Storage URLs (bucket: images, path: homepage/):
-- INSERT INTO homepage_images (title, description, image_url, display_order, is_active)
-- VALUES 
--     ('Student Projects Showcase', 'Highlighting innovative student engineering projects', 
--      'https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/project1.jpg', 
--      1, true),
--     
--     ('Engineering Excellence', 'Celebrating achievements in engineering and computer science', 
--      'https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/project2.jpg', 
--      2, true),
--     
--     ('Research Innovation', 'Cutting-edge research from our faculty and students', 
--      'https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/project3.jpg', 
--      3, true);

-- ============================================
-- Storage Bucket Setup
-- ============================================
-- Your images are stored in:
-- Storage → images (bucket) → homepage (folder/container)
-- 
-- Storage URL format (CORRECT for your structure):
-- https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/[filename]
-- 
-- URL Structure Breakdown:
-- - /images/ = bucket name
-- - /homepage/ = folder/container name
-- - [filename] = your actual image filename
-- 
-- Example URLs (replace with your actual filenames):
-- https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/image1.jpg
-- https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/image2.png
-- 
-- To get the URL for an image:
-- 1. Go to Storage → images bucket → homepage folder
-- 2. Click on an image file
-- 3. Copy the "Public URL" shown (it will match the format above)
-- 4. Use that exact URL in the INSERT statements above
-- ============================================

