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
                Name = "ACM",
                FullName = "Association for Computing Machinery",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The Association for Computing Machinery (ACM) is the world's largest educational and scientific computing society. The McNeese chapter provides opportunities for computer science students to network, learn, and grow in their field through workshops, competitions, and professional development activities.",
                ContactInfo = "Contact: acm@mcneese.edu",
                MeetingInfo = "Meetings: Every other Tuesday at 5:00 PM in Drew Hall",
                WebsiteUrl = "https://www.acm.org"
            },
            new StudentClub
            {
                Id = 2,
                Name = "ASME",
                FullName = "American Society of Mechanical Engineers",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The American Society of Mechanical Engineers (ASME) is a professional organization that promotes the art, science, and practice of mechanical engineering. The McNeese chapter offers students opportunities to connect with industry professionals, participate in design competitions, and develop leadership skills.",
                ContactInfo = "Contact: asme@mcneese.edu",
                MeetingInfo = "Meetings: First Thursday of each month at 6:00 PM",
                WebsiteUrl = "https://www.asme.org"
            },
            new StudentClub
            {
                Id = 3,
                Name = "NSBE",
                FullName = "National Society of Black Engineers",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The National Society of Black Engineers (NSBE) is one of the largest student-governed organizations in the United States. The McNeese chapter focuses on increasing the number of culturally responsible Black engineers who excel academically, succeed professionally, and positively impact the community.",
                ContactInfo = "Contact: nsbe@mcneese.edu",
                MeetingInfo = "Meetings: Every Wednesday at 4:30 PM",
                WebsiteUrl = "https://www.nsbe.org"
            },
            new StudentClub
            {
                Id = 4,
                Name = "SWE",
                FullName = "Society of Women Engineers",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The Society of Women Engineers (SWE) empowers women to achieve full potential in careers as engineers and leaders. The McNeese chapter provides networking opportunities, mentorship programs, and professional development workshops to support women in engineering.",
                ContactInfo = "Contact: swe@mcneese.edu",
                MeetingInfo = "Meetings: Second and fourth Monday of each month at 5:00 PM",
                WebsiteUrl = "https://www.swe.org"
            },
            new StudentClub
            {
                Id = 5,
                Name = "Robotics",
                FullName = "McNeese Robotics Club",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The McNeese Robotics Club brings together students interested in robotics, automation, and mechatronics. Members work on hands-on projects, participate in competitions, and collaborate on innovative robotic solutions. The club welcomes students from all engineering disciplines.",
                ContactInfo = "Contact: robotics@mcneese.edu",
                MeetingInfo = "Meetings: Every Friday at 3:00 PM in the Engineering Lab",
                WebsiteUrl = ""
            },
            new StudentClub
            {
                Id = 6,
                Name = "AIChE",
                FullName = "American Institute of Chemical Engineers",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The American Institute of Chemical Engineers (AIChE) is the world's leading organization for chemical engineering professionals. The McNeese chapter provides students with opportunities to network with industry leaders, attend conferences, and gain practical experience through projects and competitions.",
                ContactInfo = "Contact: aiche@mcneese.edu",
                MeetingInfo = "Meetings: Third Tuesday of each month at 5:30 PM",
                WebsiteUrl = "https://www.aiche.org"
            },
            new StudentClub
            {
                Id = 7,
                Name = "ACS",
                FullName = "American Chemical Society",
                ImageUrl = "mcneeselogo.png", // Placeholder - update with actual club logo
                Description = "The American Chemical Society (ACS) is the world's largest scientific society dedicated to advancing chemistry. The McNeese student chapter offers opportunities for chemistry and chemical engineering students to engage in research, attend national meetings, and connect with professionals in the field.",
                ContactInfo = "Contact: acs@mcneese.edu",
                MeetingInfo = "Meetings: First Monday of each month at 4:00 PM",
                WebsiteUrl = "https://www.acs.org"
            }
        };
    }
}

