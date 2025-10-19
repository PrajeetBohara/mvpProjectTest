-- Complete setup script for E-Week Gallery
-- Run this in your Supabase SQL Editor

-- 1. Create the table
CREATE TABLE IF NOT EXISTS public.eweek_gallery (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    year INTEGER NOT NULL,
    title TEXT NOT NULL,
    description TEXT,
    image_url TEXT NOT NULL,
    thumbnail_url TEXT,
    category TEXT CHECK (category IN ('events', 'winners', 'projects', 'ceremony', 'general')),
    display_order INTEGER DEFAULT 0,
    is_featured BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- 2. Create index for performance
CREATE INDEX IF NOT EXISTS idx_eweek_gallery_year ON public.eweek_gallery(year);

-- 3. Enable Row Level Security
ALTER TABLE public.eweek_gallery ENABLE ROW LEVEL SECURITY;

-- 4. Drop existing policy if it exists
DROP POLICY IF EXISTS "E-Week gallery is publicly readable" ON public.eweek_gallery;

-- 5. Create public read access policy
CREATE POLICY "E-Week gallery is publicly readable" ON public.eweek_gallery
    FOR SELECT USING (true);

-- 6. Insert test data for 2024
INSERT INTO public.eweek_gallery (year, title, description, image_url, category, display_order, is_featured) VALUES
(2024, 'Engineering Competition Winners', 'Team Innovation Squad wins first place in the design challenge', 'https://via.placeholder.com/400x300/4CAF50/white?text=Competition+Winners', 'winners', 1, true),
(2024, 'Guest Speaker Presentation', 'Dr. Sarah Chen discussing AI in Engineering', 'https://via.placeholder.com/400x300/2196F3/white?text=Guest+Speaker', 'events', 2, true),
(2024, 'Student Project Showcase', 'Students presenting their innovative engineering solutions', 'https://via.placeholder.com/400x300/FF9800/white?text=Project+Showcase', 'projects', 3, false),
(2024, 'Awards Ceremony', 'Recognition ceremony for outstanding achievements', 'https://via.placeholder.com/400x300/9C27B0/white?text=Awards+Ceremony', 'ceremony', 4, true),
(2024, 'Robotics Competition', 'Teams competing in the annual robotics challenge', 'https://via.placeholder.com/400x300/E91E63/white?text=Robotics+Competition', 'events', 5, false),
(2024, 'Networking Event', 'Students connecting with industry professionals', 'https://via.placeholder.com/400x300/607D8B/white?text=Networking+Event', 'events', 6, false);

-- 7. Insert test data for 2025
INSERT INTO public.eweek_gallery (year, title, description, image_url, category, display_order, is_featured) VALUES
(2025, 'Opening Ceremony', 'E-Week 2025 kicks off with an exciting opening ceremony', 'https://via.placeholder.com/400x300/4CAF50/white?text=Opening+Ceremony', 'ceremony', 1, true),
(2025, 'Green Engineering Contest', 'Teams showcasing sustainable engineering solutions', 'https://via.placeholder.com/400x300/8BC34A/white?text=Green+Engineering', 'events', 2, true),
(2025, 'Coding Marathon', '24-hour programming competition in progress', 'https://via.placeholder.com/400x300/FF5722/white?text=Coding+Marathon', 'events', 3, false),
(2025, 'Industry Panel Discussion', 'Experts discussing future trends in engineering', 'https://via.placeholder.com/400x300/3F51B5/white?text=Panel+Discussion', 'events', 4, false),
(2025, 'Student Innovation Awards', 'Recognition for most innovative student projects', 'https://via.placeholder.com/400x300/FF9800/white?text=Innovation+Awards', 'winners', 5, true),
(2025, 'Tech Expo', 'Showcasing cutting-edge technology and research', 'https://via.placeholder.com/400x300/9C27B0/white?text=Tech+Expo', 'events', 6, false);

-- 8. Verify the data was inserted
SELECT '2024 Images:' as info, COUNT(*) as count FROM public.eweek_gallery WHERE year = 2024
UNION ALL
SELECT '2025 Images:' as info, COUNT(*) as count FROM public.eweek_gallery WHERE year = 2025
UNION ALL
SELECT 'Featured 2024:' as info, COUNT(*) as count FROM public.eweek_gallery WHERE year = 2024 AND is_featured = true
UNION ALL
SELECT 'Featured 2025:' as info, COUNT(*) as count FROM public.eweek_gallery WHERE year = 2025 AND is_featured = true;
