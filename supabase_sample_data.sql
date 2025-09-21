-- =============================================
-- SAMPLE DATA FOR DASHBOARD TESTING
-- =============================================

-- Insert sample profiles (these would normally come from Supabase Auth)
INSERT INTO public.profiles (id, email, full_name, role, department) VALUES
    ('11111111-1111-1111-1111-111111111111', 'admin@mcneese.edu', 'Admin User', 'admin', 'IT'),
    ('22222222-2222-2222-2222-222222222222', 'john.doe@mcneese.edu', 'Dr. John Doe', 'faculty', 'Computer Science'),
    ('33333333-3333-3333-3333-333333333333', 'jane.smith@mcneese.edu', 'Dr. Jane Smith', 'faculty', 'Engineering'),
    ('44444444-4444-4444-4444-444444444444', 'student1@mcneese.edu', 'Alice Johnson', 'student', 'Computer Science'),
    ('55555555-5555-5555-5555-555555555555', 'student2@mcneese.edu', 'Bob Wilson', 'student', 'Engineering');

-- Insert faculty
INSERT INTO public.faculty (profile_id, title, office_location, office_hours, phone, bio, research_interests, education) VALUES
    ('22222222-2222-2222-2222-222222222222', 'Professor', 'KCS 201', 'Mon-Fri 10:00-12:00', '337-475-5000', 'Expert in machine learning and artificial intelligence.', 
     ARRAY['Machine Learning', 'Computer Vision', 'Natural Language Processing'], 
     ARRAY['PhD Computer Science - MIT', 'MS Computer Science - Stanford']),
    ('33333333-3333-3333-3333-333333333333', 'Associate Professor', 'KCS 205', 'Mon-Fri 2:00-4:00', '337-475-5001', 'Specializes in software engineering and database systems.',
     ARRAY['Software Engineering', 'Database Systems', 'Web Development'],
     ARRAY['PhD Software Engineering - Carnegie Mellon', 'MS Computer Science - UC Berkeley']);

-- Insert clubs
INSERT INTO public.clubs (name, description, president_id, advisor_id, meeting_schedule, contact_email, social_links) VALUES
    ('Computer Science Club', 'A student organization focused on programming, technology, and career development.', 
     '44444444-4444-4444-4444-444444444444', 
     (SELECT id FROM public.faculty WHERE profile_id = '22222222-2222-2222-2222-222222222222'),
     'Every Tuesday 6:00 PM', 'csclub@mcneese.edu', 
     '{"instagram": "https://instagram.com/mcneese_cs", "discord": "https://discord.gg/mcneese-cs"}'),
    ('Engineering Society', 'Promoting engineering excellence and professional development.', 
     '55555555-5555-5555-5555-555555555555',
     (SELECT id FROM public.faculty WHERE profile_id = '33333333-3333-3333-3333-333333333333'),
     'Every Thursday 7:00 PM', 'engsociety@mcneese.edu',
     '{"facebook": "https://facebook.com/mcneese-eng", "linkedin": "https://linkedin.com/company/mcneese-eng"}');

-- Insert events
INSERT INTO public.events (title, description, event_date, end_date, location, club_id, organizer_id, max_attendees, registration_required, is_featured) VALUES
    ('Tech Talk: AI in Healthcare', 'Join us for an exciting discussion about artificial intelligence applications in healthcare.', 
     '2024-02-15 18:00:00+00', '2024-02-15 20:00:00+00', 'KCS Auditorium', 
     (SELECT id FROM public.clubs WHERE name = 'Computer Science Club'),
     '44444444-4444-4444-4444-444444444444', 100, true, true),
    ('Engineering Career Fair', 'Meet with top engineering companies and explore career opportunities.', 
     '2024-03-20 10:00:00+00', '2024-03-20 16:00:00+00', 'Student Union Ballroom',
     (SELECT id FROM public.clubs WHERE name = 'Engineering Society'),
     '55555555-5555-5555-5555-555555555555', 500, true, true),
    ('Hackathon 2024', '48-hour coding competition with prizes and networking opportunities.', 
     '2024-04-05 09:00:00+00', '2024-04-07 17:00:00+00', 'KCS Building',
     (SELECT id FROM public.clubs WHERE name = 'Computer Science Club'),
     '44444444-4444-4444-4444-444444444444', 50, true, false);

-- Insert announcements
INSERT INTO public.announcements (title, content, author_id, priority, is_published, published_at) VALUES
    ('Spring 2024 Registration Opens', 'Registration for Spring 2024 courses will open on November 1st at 8:00 AM. Please check your degree plan and meet with your advisor.', 
     '11111111-1111-1111-1111-111111111111', 'high', true, NOW()),
    ('New Computer Lab Equipment', 'The KCS building has received new high-performance workstations for student use. Lab hours are 8 AM - 10 PM Monday through Friday.', 
     '22222222-2222-2222-2222-222222222222', 'normal', true, NOW()),
    ('Scholarship Applications Due', 'Engineering and Computer Science scholarship applications are due by March 15th. Apply online through the student portal.', 
     '33333333-3333-3333-3333-333333333333', 'urgent', true, NOW());

-- Insert projects
INSERT INTO public.projects (title, description, team_members, advisor_id, project_type, status, start_date, end_date, is_featured) VALUES
    ('Smart Campus Navigation System', 'An AI-powered mobile app that helps students navigate campus using augmented reality and real-time data.', 
     '[{"name": "Alice Johnson", "role": "Lead Developer"}, {"name": "Bob Wilson", "role": "UI/UX Designer"}, {"name": "Charlie Brown", "role": "Backend Developer"}]',
     (SELECT id FROM public.faculty WHERE profile_id = '22222222-2222-2222-2222-222222222222'),
     'capstone', 'active', '2024-01-15', '2024-05-15', true),
    ('IoT Environmental Monitoring', 'A network of sensors to monitor air quality, temperature, and humidity across campus.', 
     '[{"name": "David Lee", "role": "Hardware Engineer"}, {"name": "Eva Martinez", "role": "Data Analyst"}, {"name": "Frank Chen", "role": "Software Developer"}]',
     (SELECT id FROM public.faculty WHERE profile_id = '33333333-3333-3333-3333-333333333333'),
     'research', 'active', '2024-02-01', '2024-08-01', true),
    ('E-Learning Platform', 'A comprehensive online learning management system with video streaming and interactive assessments.', 
     '[{"name": "Grace Kim", "role": "Full Stack Developer"}, {"name": "Henry Davis", "role": "Database Administrator"}]',
     (SELECT id FROM public.faculty WHERE profile_id = '22222222-2222-2222-2222-222222222222'),
     'industry', 'completed', '2023-09-01', '2023-12-15', false);

-- Insert gallery items
INSERT INTO public.gallery (title, description, image_url, category, uploaded_by, tags, is_featured) VALUES
    ('Hackathon 2023 Winners', 'Team Alpha celebrating their victory at the annual hackathon.', 
     'https://example.com/images/hackathon-winners.jpg', 'events', 
     '44444444-4444-4444-4444-444444444444', ARRAY['hackathon', 'winners', '2023'], true),
    ('Senior Design Project Demo', 'Students presenting their capstone project to industry judges.', 
     'https://example.com/images/senior-design-demo.jpg', 'projects', 
     '55555555-5555-5555-5555-555555555555', ARRAY['senior-design', 'presentation', 'demo'], true),
    ('Engineering Career Fair 2023', 'Students networking with industry professionals.', 
     'https://example.com/images/career-fair-2023.jpg', 'events', 
     '33333333-3333-3333-3333-333333333333', ARRAY['career-fair', 'networking', '2023'], false);

-- Insert courses
INSERT INTO public.courses (course_code, course_name, description, credits, prerequisites, department, level) VALUES
    ('CS 101', 'Introduction to Computer Science', 'Fundamental concepts of computer science and programming.', 3, ARRAY[]::TEXT[], 'Computer Science', 'undergraduate'),
    ('CS 201', 'Data Structures and Algorithms', 'Study of fundamental data structures and algorithmic problem-solving techniques.', 3, ARRAY['CS 101'], 'Computer Science', 'undergraduate'),
    ('CS 401', 'Software Engineering', 'Principles and practices of software development lifecycle.', 3, ARRAY['CS 201'], 'Computer Science', 'undergraduate'),
    ('ENG 101', 'Introduction to Engineering', 'Overview of engineering disciplines and professional practice.', 3, ARRAY[]::TEXT[], 'Engineering', 'undergraduate'),
    ('ENG 301', 'Engineering Design', 'Capstone design project course.', 3, ARRAY['ENG 101'], 'Engineering', 'undergraduate');

-- Insert sponsors
INSERT INTO public.sponsors (name, logo_url, website_url, contact_person, contact_email, sponsorship_level, contribution_amount, contribution_type, description) VALUES
    ('TechCorp Industries', 'https://example.com/logos/techcorp.png', 'https://techcorp.com', 'John Smith', 'john.smith@techcorp.com', 'platinum', 50000.00, 'monetary', 'Leading technology company supporting engineering education.'),
    ('DataSoft Solutions', 'https://example.com/logos/datasoft.png', 'https://datasoft.com', 'Sarah Johnson', 'sarah.johnson@datasoft.com', 'gold', 25000.00, 'monetary', 'Software company providing internship opportunities.'),
    ('InnovateLab', 'https://example.com/logos/innovatelab.png', 'https://innovatelab.com', 'Mike Davis', 'mike.davis@innovatelab.com', 'silver', 10000.00, 'equipment', 'Research lab donating equipment and mentorship.'),
    ('FutureTech', 'https://example.com/logos/futuretech.png', 'https://futuretech.com', 'Lisa Wang', 'lisa.wang@futuretech.com', 'bronze', 5000.00, 'services', 'Startup providing guest lectures and workshops.');
