-- =============================================
-- DASHBOARD DATABASE SCHEMA FOR SUPABASE
-- =============================================

-- Enable necessary extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- =============================================
-- CORE TABLES
-- =============================================

-- Users table (extends Supabase auth.users)
CREATE TABLE public.profiles (
    id UUID REFERENCES auth.users(id) PRIMARY KEY,
    email TEXT UNIQUE NOT NULL,
    full_name TEXT,
    avatar_url TEXT,
    role TEXT DEFAULT 'student' CHECK (role IN ('admin', 'faculty', 'student', 'staff')),
    department TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Faculty directory
CREATE TABLE public.faculty (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    profile_id UUID REFERENCES public.profiles(id) ON DELETE CASCADE,
    title TEXT NOT NULL, -- Professor, Associate Professor, etc.
    office_location TEXT,
    office_hours TEXT,
    phone TEXT,
    bio TEXT,
    research_interests TEXT[],
    education TEXT[],
    publications TEXT[],
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Student clubs
CREATE TABLE public.clubs (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    logo_url TEXT,
    president_id UUID REFERENCES public.profiles(id),
    advisor_id UUID REFERENCES public.faculty(id),
    meeting_schedule TEXT,
    contact_email TEXT,
    social_links JSONB, -- Instagram, Facebook, etc.
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Events
CREATE TABLE public.events (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    event_date TIMESTAMP WITH TIME ZONE NOT NULL,
    end_date TIMESTAMP WITH TIME ZONE,
    location TEXT,
    image_url TEXT,
    club_id UUID REFERENCES public.clubs(id),
    organizer_id UUID REFERENCES public.profiles(id),
    max_attendees INTEGER,
    registration_required BOOLEAN DEFAULT false,
    registration_url TEXT,
    is_featured BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Announcements
CREATE TABLE public.announcements (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    author_id UUID REFERENCES public.profiles(id),
    priority TEXT DEFAULT 'normal' CHECK (priority IN ('low', 'normal', 'high', 'urgent')),
    is_published BOOLEAN DEFAULT false,
    published_at TIMESTAMP WITH TIME ZONE,
    expires_at TIMESTAMP WITH TIME ZONE,
    attachment_urls TEXT[],
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Senior design projects
CREATE TABLE public.projects (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT NOT NULL,
    team_members JSONB NOT NULL, -- Array of student profiles
    advisor_id UUID REFERENCES public.faculty(id),
    sponsor_company TEXT,
    project_type TEXT CHECK (project_type IN ('capstone', 'research', 'competition', 'industry')),
    status TEXT DEFAULT 'active' CHECK (status IN ('planning', 'active', 'completed', 'cancelled')),
    start_date DATE,
    end_date DATE,
    image_urls TEXT[],
    video_urls TEXT[],
    document_urls TEXT[],
    github_url TEXT,
    demo_url TEXT,
    is_featured BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Gallery
CREATE TABLE public.gallery (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    image_url TEXT NOT NULL,
    thumbnail_url TEXT,
    category TEXT NOT NULL CHECK (category IN ('events', 'projects', 'clubs', 'general', 'awards')),
    event_id UUID REFERENCES public.events(id),
    project_id UUID REFERENCES public.projects(id),
    club_id UUID REFERENCES public.clubs(id),
    uploaded_by UUID REFERENCES public.profiles(id),
    tags TEXT[],
    is_featured BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Academic catalogue
CREATE TABLE public.courses (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    course_code TEXT NOT NULL UNIQUE, -- e.g., "CS 101"
    course_name TEXT NOT NULL,
    description TEXT,
    credits INTEGER NOT NULL,
    prerequisites TEXT[],
    department TEXT NOT NULL,
    level TEXT CHECK (level IN ('undergraduate', 'graduate')),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Sponsors and donors
CREATE TABLE public.sponsors (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    name TEXT NOT NULL,
    logo_url TEXT,
    website_url TEXT,
    contact_person TEXT,
    contact_email TEXT,
    contact_phone TEXT,
    sponsorship_level TEXT CHECK (sponsorship_level IN ('platinum', 'gold', 'silver', 'bronze')),
    contribution_amount DECIMAL(10,2),
    contribution_type TEXT, -- 'monetary', 'equipment', 'services'
    description TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- =============================================
-- INDEXES FOR PERFORMANCE
-- =============================================

CREATE INDEX idx_events_date ON public.events(event_date);
CREATE INDEX idx_events_club ON public.events(club_id);
CREATE INDEX idx_announcements_published ON public.announcements(is_published, published_at);
CREATE INDEX idx_projects_status ON public.projects(status);
CREATE INDEX idx_gallery_category ON public.gallery(category);
CREATE INDEX idx_faculty_active ON public.faculty(is_active);

-- =============================================
-- ROW LEVEL SECURITY (RLS)
-- =============================================

-- Enable RLS on all tables
ALTER TABLE public.profiles ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.faculty ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.clubs ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.events ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.announcements ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.projects ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.gallery ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.courses ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.sponsors ENABLE ROW LEVEL SECURITY;

-- =============================================
-- RLS POLICIES
-- =============================================

-- Profiles: Users can read all profiles, update their own
CREATE POLICY "Public profiles are viewable by everyone" ON public.profiles
    FOR SELECT USING (true);

CREATE POLICY "Users can update their own profile" ON public.profiles
    FOR UPDATE USING (auth.uid() = id);

-- Faculty: Public read access
CREATE POLICY "Faculty directory is publicly readable" ON public.faculty
    FOR SELECT USING (is_active = true);

-- Clubs: Public read access
CREATE POLICY "Clubs are publicly readable" ON public.clubs
    FOR SELECT USING (is_active = true);

-- Events: Public read access
CREATE POLICY "Events are publicly readable" ON public.events
    FOR SELECT USING (true);

-- Announcements: Public read access for published announcements
CREATE POLICY "Published announcements are publicly readable" ON public.announcements
    FOR SELECT USING (is_published = true AND (expires_at IS NULL OR expires_at > NOW()));

-- Projects: Public read access
CREATE POLICY "Projects are publicly readable" ON public.projects
    FOR SELECT USING (true);

-- Gallery: Public read access
CREATE POLICY "Gallery is publicly readable" ON public.gallery
    FOR SELECT USING (true);

-- Courses: Public read access
CREATE POLICY "Courses are publicly readable" ON public.courses
    FOR SELECT USING (is_active = true);

-- Sponsors: Public read access
CREATE POLICY "Sponsors are publicly readable" ON public.sponsors
    FOR SELECT USING (is_active = true);

-- =============================================
-- FUNCTIONS
-- =============================================

-- Function to update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Add triggers for updated_at
CREATE TRIGGER update_profiles_updated_at BEFORE UPDATE ON public.profiles
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_faculty_updated_at BEFORE UPDATE ON public.faculty
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_clubs_updated_at BEFORE UPDATE ON public.clubs
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_events_updated_at BEFORE UPDATE ON public.events
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_announcements_updated_at BEFORE UPDATE ON public.announcements
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_projects_updated_at BEFORE UPDATE ON public.projects
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_courses_updated_at BEFORE UPDATE ON public.courses
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_sponsors_updated_at BEFORE UPDATE ON public.sponsors
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
