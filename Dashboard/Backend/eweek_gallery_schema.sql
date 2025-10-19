-- E-Week Gallery Schema for Supabase
-- Add this to your existing supabase_schema.sql or run separately

-- E-Week gallery table for photo galleries only
CREATE TABLE public.eweek_gallery (
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

-- Index for performance
CREATE INDEX idx_eweek_gallery_year ON public.eweek_gallery(year);

-- Row Level Security
ALTER TABLE public.eweek_gallery ENABLE ROW LEVEL SECURITY;

-- Public read access policy
CREATE POLICY "E-Week gallery is publicly readable" ON public.eweek_gallery
    FOR SELECT USING (true);
