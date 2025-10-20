-- Sample data for E-Week Gallery
-- Here image data will be replaced by the url form supabase

-- Sample images for E-Week 2024
INSERT INTO public.eweek_gallery (year, title, description, image_url, category, display_order, is_featured) VALUES
(2024, 'Engineering Competition Winners', 'Team Innovation Squad wins first place in the design challenge', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/winners1.jpg', 'winners', 1, true),
(2024, 'Guest Speaker Presentation', 'Dr. Sarah Chen discussing AI in Engineering', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/speaker1.jpg', 'events', 2, true),
(2024, 'Student Project Showcase', 'Students presenting their innovative engineering solutions', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/projects1.jpg', 'projects', 3, false),
(2024, 'Awards Ceremony', 'Recognition ceremony for outstanding achievements', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/ceremony1.jpg', 'ceremony', 4, true),
(2024, 'Robotics Competition', 'Teams competing in the annual robotics challenge', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/robotics1.jpg', 'events', 5, false),
(2024, 'Networking Event', 'Students connecting with industry professionals', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2024/networking1.jpg', 'events', 6, false);

-- Sample images for E-Week 2025
INSERT INTO public.eweek_gallery (year, title, description, image_url, category, display_order, is_featured) VALUES
(2025, 'Opening Ceremony', 'E-Week 2025 kicks off with an exciting opening ceremony', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/opening1.jpg', 'ceremony', 1, true),
(2025, 'Green Engineering Contest', 'Teams showcasing sustainable engineering solutions', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/green1.jpg', 'events', 2, true),
(2025, 'Coding Marathon', '24-hour programming competition in progress', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/coding1.jpg', 'events', 3, false),
(2025, 'Industry Panel Discussion', 'Experts discussing future trends in engineering', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/panel1.jpg', 'events', 4, false),
(2025, 'Student Innovation Awards', 'Recognition for most innovative student projects', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/awards1.jpg', 'winners', 5, true),
(2025, 'Tech Expo', 'Showcasing cutting-edge technology and research', 'https://your-supabase-url/storage/v1/object/public/images/eweek/2025/tech1.jpg', 'events', 6, false);

