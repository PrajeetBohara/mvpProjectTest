// Code written for StudentClubService to manage student club data
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for managing student club information.
/// </summary>
public class StudentClubService
{
    private readonly List<StudentClub> _clubs;

    /// <summary>
    /// Initializes the StudentClubService with club data.
    /// </summary>
    public StudentClubService()
    {
        _clubs = InitializeClubs();
    }

    /// <summary>
    /// Gets all available student clubs.
    /// </summary>
    /// <returns>List of all student clubs.</returns>
    public List<StudentClub> GetAllClubs()
    {
        return _clubs;
    }

    /// <summary>
    /// Gets a specific club by ID.
    /// </summary>
    /// <param name="id">The club ID.</param>
    /// <returns>The club if found, otherwise null.</returns>
    public StudentClub? GetClubById(int id)
    {
        return _clubs.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Initializes the list of student clubs with their information.
    /// </summary>
    private List<StudentClub> InitializeClubs()
    {
        return new List<StudentClub>
        {
            new StudentClub
            {
                Id = 1,
                Name = "ASCE",
                FullName = "American Society of Civil Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "ASCE is the oldest national professional engineering society, and was founded in 1852. Their mission is \"to enhance the welfare of humanity by advancing the science and profession of civil engineering.\" There are more than 150,000 members in 176 countries. Here's a few things ASCE does:\n\n• Develops guidelines, codes, and standards.\n• Assists in the development of civil engineering education curricula and accreditation.\n• Promotes and provides continuing education for civil engineers.\n• Enhances the public image of civil engineers.\n\nASCE is the voice of the profession. ASCE helps members shape public policy to build better communities.\n\nThe McNeese student chapter was chartered in 1982, and operates in the Louisiana Section. Our chapter provides students with an excellent opportunity to develop the leadership and organizational abilities that they will use throughout their careers, and seeks to provide a training ground for the future leadership of ASCE and the profession. McNeese ASCE has a proud history of active participation in conference and national competition, professional events, and community service.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "6:00 PM",
                MeetingLocation = "Drew 125",
                YearEstablished = 1852,
                Mission = "To enhance the welfare of humanity by advancing the science and profession of civil engineering.",
                Vision = "To be the voice of the profession and help members shape public policy to build better communities.",
                Values = "Professionalism, Leadership, Community Service, Education",
                WebsiteUrl = "https://www.asce.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/asce",
                TwitterUrl = "",
                // ============================================
                // CLUB LOGO AND GALLERY IMAGES FROM SUPABASE
                // ============================================
                // Copy the Public URL from Supabase Storage (images/clubs folder)
                // Format: https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/[filename]
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ace.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asce/20250307_160720.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asce/concrete%20canoe%2021.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asce/concrete%20canoe%205.jpg"
                }
            },
            new StudentClub
            {
                Id = 2,
                Name = "ASME",
                FullName = "American Society of Mechanical Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "The American Society of Mechanical Engineers exists to provide the necessary tools for students to grow in their personal and professional engineering journey. We offer opportunities for our members to engage with like-minded students to build long-lasting connections in and out of the industry. Our goals are to expand our reach across the engineering community and provide opportunities for our members to have first-hand industry experience.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "Bi-Weekly",
                MeetingLocation = "Drew 107",
                YearEstablished = 1880,
                Mission = "To have a stress-free involvement with the same benefits as leading organizations here at McNeese.",
                Vision = "ASME helps students grow and prepare for the real world.",
                Values = "Integrity, Education, Experience",
                WebsiteUrl = "https://www.asme.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/asme",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asme.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asme/1763246711613.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asme/thumbnail_img_0389.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/asme/thumbnail_img_9016.jpg"
                }
            },
            new StudentClub
            {
                Id = 3,
                Name = "AIChE",
                FullName = "American Institute of Chemical Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "The American Institute of Chemical Engineers (AIChE) McNeese chapter is focused on giving a full array of educational and training resources to student chemical engineers. Throughout the semester, AIChE will bring the students closer to industry and give students multiple opportunities to grow academically and push their knowledge. Through research programs or projects such as our ChemE Car team students will be given multiple ways to better themselves academically and personally.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "5:00 PM",
                MeetingLocation = "Drew Hall",
                YearEstablished = null,
                Mission = "To provide educational and training resources to student chemical engineers and connect them with industry.",
                Vision = "To help students grow academically and personally through research programs and projects.",
                Values = "Education, Innovation, Professional Development",
                WebsiteUrl = "https://www.aiche.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/aiche",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/aiche.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/aiche/0.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/aiche/dsc08340.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/aiche/screenshot%202025-05-23%20084542.jpg"
                }
            },
            new StudentClub
            {
                Id = 4,
                Name = "ACM",
                FullName = "Association for Computing Machinery",
                ImageUrl = "mcneeselogo.png",
                Description = "Welcome to our vibrant community of aspiring computer scientists and tech enthusiasts! Here at McNeese's Association of Computer Machinery, we are dedicated to fostering creativity, innovation, and a deep passion for all things tech-related. Whether you're an experienced coder or just starting your journey into the world of programming, you'll find a home among peers who share your curiosity and drive.\n\nOur club thrives on the energy and ingenuity of our members, who lead projects ranging from cutting-edge software development to exploring the intersections of technology with fields like art, business, and beyond. We believe in the power of hands-on learning and collaborative problem-solving, where every member has the opportunity to not only learn from but also contribute to exciting projects.\n\nBeyond technical skills, we aim to elevate the visibility and impact of computer science within our community. Through outreach events, workshops, and partnerships with local organizations, we're actively promoting the computer science degree and its limitless potential in shaping the future.\n\nJoin us as we embark on a journey of exploration, learning, and innovation. Together, let's build, create, and inspire the next generation of leaders in computer science right here in Lake Charles. Whether your interest lies in artificial intelligence, cybersecurity, app development, or something entirely new, there's a place for you at McNeese's Association of Computer Machinery. Let's code the future together!\n\nAll you need is an interest in tech. Learn with us!\n\nWe host workshops, programming competitions, and organize student-led projects and events that benefit our community!",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "Wednesdays Biweekly, Wednesday 03:00 PM",
                MeetingLocation = "DREW 317 (HACKERSPACE)",
                YearEstablished = null,
                Mission = "Create a hub for Computer Science related students and activities",
                Vision = "Promote and improve the computer science community within McNeese State University",
                Values = "Teamwork, Innovation, Integrity",
                WebsiteUrl = "https://www.acm.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/acm",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ACM%20Logo%20(Spring%2025%20Update)%20White%20Background.png",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/acm/IMG_5296.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/acm/IMG_5829.JPG",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/acm/IMG_7949.JPEG"
                }
            },
            new StudentClub
            {
                Id = 5,
                Name = "NSBE",
                FullName = "National Society of Black Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "The National Society of Black Engineers (NSBE) is one of the largest student governed organizations in the world. We have birthed hundreds of businesses, trained thousands of corporate leaders, graduated tens of thousands of engineers, and engaged hundreds of thousands of K-12 students in STEM education over our 47-year history, both nationally and abroad. In short, NSBE members, chapters, and supporters are dedicated to increasing the number of culturally responsible Black Engineers who excel academically, succeed professionally and positively impact the community.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "5:30 PM - Wednesday Biweekly",
                MeetingLocation = "Drew Hall Room 125/126",
                YearEstablished = 1975,
                Mission = "To increase the number of culturally responsible Black Engineers who excel academically, succeed professionally and positively impact the community. Leadership, Technical Excellence, Academic Excellence and Mentoring are all a part of how we positively impact the community. Supporting NSBE means that you are working to build a strong legacy of leaders, innovators and skilled global citizens that will positively impact the world for decades to come!",
                Vision = "We envision a world in which engineering is a mainstream word in homes and communities of color, and all Black students can envision themselves as engineers. In this world, Blacks exceed parity in entering engineering fields, earning degrees, and succeeding professionally.",
                Values = "Leadership, Technical Excellence, Academic Excellence, Mentoring, Community Impact",
                WebsiteUrl = "https://www.nsbe.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/national-society-of-black-engineers",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/nsbe.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/nsbe/1729183370579.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/nsbe/McNeese-NSBE-4-e1649160407611-1024x749.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/nsbe/NSBEGroup-copy-1024x576.jpg"
                }
            },
            new StudentClub
            {
                Id = 6,
                Name = "SWE",
                FullName = "Society of Women Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "The Society of Women Engineers is a woman-led national organization set to empower and give a voice to the women in the field of engineering.\n\nIn SWE at McNeese, we are a sisterhood of like-minded women who are there to help, teach, and encourage each other to be the best engineers - first in the classroom and then in the industry.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "Contact club for meeting schedule",
                MeetingLocation = "Contact club for location",
                YearEstablished = null,
                Mission = "To empower and give a voice to women in the field of engineering",
                Vision = "To create a sisterhood that helps, teaches, and encourages women to be the best engineers in the classroom and industry",
                Values = "Empowerment, Support, Professional Development, Sisterhood",
                WebsiteUrl = "https://www.swe.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/society-of-women-engineers",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/swe.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/swe/1758991830332.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/swe/1759073056039.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/swe/1759602660387.jpg"
                }
            },
            new StudentClub
            {
                Id = 7,
                Name = "Robotics",
                FullName = "McNeese Robotics",
                ImageUrl = "mcneeselogo.png",
                Description = "McNeese Robotics is a student organization centered around the VEX Robotics Competition. Each year, our team takes on a new challenge that requires us to design, build, and program two robots to solve a unique problem.\n\nAt the beginning of the semester, we provide hands-on training for new members, teaching essential skills such as robot design, chassis construction, and programming through our Herobot Competition. In the Herobot Competition, club members are split into four teams, and they are tasked with building and programming the Herobot, which is a robot designed by VEX for the game of the year. Once the teams have their robots built, teams are encouraged to modify their robots to make them more effective at scoring points by completing objectives. Around the middle of the semester, the teams will compete against each other in a tournament.\n\nAfter the Herobot Competition, club members are encouraged to join the Competition Team to design, build, and programming two robots to compete against other universities in tournaments across the country. We compete with teams from LSU, University of Texas, Texas A&M, Texas Tech, LA Tech, Arkansas Tech, and many more.\n\nClub members are also encouraged to volunteer at local competitions to meet other teams, promote our club, and educate the next generation!\n\nOur foundation is built on education and competition, giving students the opportunity to develop technical expertise, creativity, and teamwork through robotics.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "Friday 3:00 PM",
                MeetingLocation = "DREW 304",
                YearEstablished = 2019,
                Mission = "To inspire and equip students through hands-on robotics challenges, fostering technical skills, teamwork, and problem-solving that prepare them for future opportunities in STEM fields.",
                Vision = "A community where every student has the confidence, creativity, and knowledge to shape the future of robotics and technology.",
                Values = "Innovation, Collaboration, Perseverance, Integrity, and Growth",
                WebsiteUrl = "",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/robotics.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/robotics/herobot7%20upload.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/robotics/img_2669.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/robotics/robotics%202024%2011.jpg"
                }
            },
            new StudentClub
            {
                Id = 8,
                Name = "Automotive",
                FullName = "Automotive Engineering Club",
                ImageUrl = "mcneeselogo.png",
                Description = "The purpose of this organization is to provide its members opportunities to gain broader insight into the engineering profession, deepen and discover their love for the automotive industry, and also gain skills and knowledge of the trade.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "5:30 PM - Tuesdays",
                MeetingLocation = "ETL 114",
                YearEstablished = null,
                Mission = "To provide opportunities for members to gain insight into the engineering profession and automotive industry",
                Vision = "To help members discover and deepen their love for the automotive industry while gaining valuable skills and knowledge",
                Values = "Education, Hands-on Learning, Professional Development",
                WebsiteUrl = "",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/automotive.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/automotive/automotive(24of109).jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/automotive/automotive(53of109).jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/automotive/dsc02510.jpg"
                }
            },
            new StudentClub
            {
                Id = 9,
                Name = "IEEE",
                FullName = "Institute of Electrical and Electronics Engineers",
                ImageUrl = "mcneeselogo.png",
                Description = "The IEEE Student Club at our university is dedicated to fostering the growth of students interested in electrical engineering, computer science, and related fields. We provide opportunities for hands-on experience through workshops, technical projects, and competitions. Members also benefit from networking with professionals, industry visits, and career development events. The club encourages collaboration, innovation, and staying up-to-date with emerging technologies, preparing students for successful careers in the tech world.",
                ContactInfo = "Contact the club for more information",
                MeetingTime = "5:30 PM",
                MeetingLocation = "Drew 327",
                YearEstablished = 2024,
                Mission = "The mission of our IEEE Student Club is to empower students by providing a platform for learning, innovation, and professional growth in the fields of electrical engineering and technology. We aim to bridge the gap between theoretical knowledge and practical application through hands-on projects, workshops, and industry collaborations. Our goal is to cultivate leadership, technical expertise, and a passion for engineering, preparing students to excel in their future careers while contributing positively to society through technological advancements.",
                Vision = "Our vision for the IEEE Student Club is to be a leading community of aspiring engineers and technologists who drive innovation and create impactful solutions to real-world challenges. We strive to cultivate a culture of collaboration, continuous learning, and professional development, empowering students to become leaders in their fields. Through our efforts, we aim to inspire future generations of engineers to use their skills to make a positive difference in society and contribute to the advancement of technology globally.",
                Values = "Innovation – We encourage creativity and forward-thinking to develop cutting-edge solutions to today's challenges.\n\nCollaboration – We foster teamwork and open communication, recognizing that the best ideas come from working together.\n\nIntegrity – We uphold ethical standards in all our endeavors, ensuring honesty, fairness, and transparency.\n\nExcellence – We are committed to continuous improvement and striving for the highest level of quality in our projects and learning.\n\nInclusion – We embrace diversity and create an inclusive environment where every member is valued and has the opportunity to contribute.",
                WebsiteUrl = "https://www.ieee.org",
                FacebookUrl = "",
                InstagramUrl = "",
                LinkedInUrl = "https://www.linkedin.com/company/ieee",
                TwitterUrl = "",
                LogoUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ieee.jpg",
                GalleryImageUrls = new List<string>
                {
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ieee/490773278_1243009567834183_6252974109630189574_n.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ieee/490912772_1243009257834214_4960233341895513130_n.jpg",
                    "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/clubs/ieee/502466746_2920961871420176_2329014704912817066_n.jpg"
                }
            }
        };
    }
}
