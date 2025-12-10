// Code written for SponsorDonorService to manage sponsors and donors data
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for managing sponsors and donors information.
/// </summary>
public class SponsorDonorService
{
    private readonly List<SponsorDonor> _sponsorsDonors;
    private readonly List<SponsorDonorImage> _galleryImages;

    /// <summary>
    /// Initializes the SponsorDonorService with sponsor and donor data.
    /// </summary>
    public SponsorDonorService()
    {
        _sponsorsDonors = InitializeSponsorsDonors();
        _galleryImages = InitializeGalleryImages();
    }

    /// <summary>
    /// Gets all corporate sponsors and donors.
    /// </summary>
    /// <returns>List of corporate sponsors and donors.</returns>
    public List<SponsorDonor> GetCorporateSponsors()
    {
        return _sponsorsDonors.Where(s => s.Type == SponsorDonorType.Corporate).ToList();
    }

    /// <summary>
    /// Gets all individual and organizational donors.
    /// </summary>
    /// <returns>List of individual and organizational donors.</returns>
    public List<SponsorDonor> GetOtherDonors()
    {
        return _sponsorsDonors.Where(s => s.Type != SponsorDonorType.Corporate).ToList();
    }

    /// <summary>
    /// Gets all sponsors and donors.
    /// </summary>
    /// <returns>List of all sponsors and donors.</returns>
    public List<SponsorDonor> GetAllSponsorsDonors()
    {
        return _sponsorsDonors;
    }

    /// <summary>
    /// Gets all gallery images.
    /// </summary>
    /// <returns>List of all gallery images.</returns>
    public List<SponsorDonorImage> GetGalleryImages()
    {
        return _galleryImages;
    }

    /// <summary>
    /// Initializes the list of sponsors and donors.
    /// </summary>
    private List<SponsorDonor> InitializeSponsorsDonors()
    {
        return new List<SponsorDonor>
        {
            // Corporate Sponsors and Donors
            new SponsorDonor
            {
                Name = "Westlake Corporation",
                Description = "Has provided support through multiple donations for the engineering endowment and the student study center, with one donation totaling $185,000 for the study center.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Phillips 66",
                Description = "Has donated funds for student chapters and the unit operations laboratory.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Cameron LNG",
                Description = "A significant donor for scholarships and other initiatives.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Entergy Solutions",
                Description = "Donated funds for energy-efficient upgrades.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Tellurian",
                Description = "Provided a substantial pledge to support the LNG Center of Excellence and scholarships for the LNG Business Certificate program.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Brask Inc.",
                Description = "Donated to establish an engineering scholarship.",
                Type = SponsorDonorType.Corporate
            },
            new SponsorDonor
            {
                Name = "Turner Industries",
                Description = "Committed funds through the \"First Choice\" campaign to support high-demand academic areas like engineering.",
                Type = SponsorDonorType.Corporate
            },

            // Other Donors
            new SponsorDonor
            {
                Name = "Contractors' Educational Trust Fund (CETF)",
                Description = "Donated $50,000 to support engineering programs and construction management curriculum.",
                Type = SponsorDonorType.Organization
            },
            new SponsorDonor
            {
                Name = "Market Basket Charitable Foundation",
                Description = "Provided a $10,000 donation for a scholarship.",
                Type = SponsorDonorType.Organization
            },
            new SponsorDonor
            {
                Name = "Karen and Ken Chamberlain",
                Description = "Donated to the Dr. Thomas S. Leary Past President Engineering Scholarship.",
                Type = SponsorDonorType.Individual
            },
            new SponsorDonor
            {
                Name = "Tommie and Jeffery Schweitzer",
                Description = "Donated to establish the Schweitzer Environmental Engineering Scholarship for students pursuing a minor in environmental engineering.",
                Type = SponsorDonorType.Individual
            },
            new SponsorDonor
            {
                Name = "Pitt Grill Inc.",
                Description = "Donated to establish the Pitt Grill Business Scholarship.",
                Type = SponsorDonorType.Organization
            }
        };
    }

    /// <summary>
    /// Initializes the list of gallery images.
    /// </summary>
    private List<SponsorDonorImage> InitializeGalleryImages()
    {
        return new List<SponsorDonorImage>
        {
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Tellurian.jpg", // Placeholder - update with actual URL
                Description = "Tellurian presented their $1 million pledge in support of the LNG Center of Excellence at McNeese State University and student scholarships for the new McNeese LNG Business Certificate. Pictured left to right are: Joey Mahmoud, Tellurian Senior Vice President of Tellurian Pipeline Development and Driftwood Pipeline LLC President; Dr. Shuming Bai, Dean McNeese College of Business; Dr. Srinivasan Ambatipati, Department Head of Engineering and Computer Science, McNeese College of Science, Engineering and Mathematics; Dr. Daryl Burckel, McNeese State University President; Samik Mukherjee, Tellurian Executive Vice President and Driftwood Assets President; Joi Lecznar, Tellurian Executive Vice President of Public and Government Affairs; Dr. Chip LeMieux, McNeese State University Provost and Vice President for Academic Affairs and Enrollment Management; and Dr. Wade Rousse, McNeese State University Executive Vice President."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Phillips66.jpg", // Placeholder - update with actual URL
                Description = "Phillips 66 has donated $25,000 to the McNeese State University College of Engineering and Computer Science through the McNeese Foundation."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/MarketBasket%20(1).jpg", // Placeholder - update with actual URL
                Description = "The Market Basket Charitable Foundation has donated $10,000 to the McNeese State University Foundation for the Market Basket Academic Scholarship. The funds for the donation were raised at the Market Basket Golf Tournament recently held at The National Golf Club in Westlake, La. On hand for the presentation are from left: Greg Franz, Market Basket vice president of grocery operations; Jennifer Leger, McNeese Foundation director of operations; Russell Saleme, Market Basket vice president of sales and marketing; Skylar Thompson, Market Basket president/CEO; and David Thompson, Market Basket vice president of perishables."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/WestlakeCorporation.png", // Placeholder - update with actual URL
                Description = "Westlake Corporation has donated $20,000 to the McNeese State University Foundation for the engineering endowment in the McNeese College of Engineering and Sciences. On hand for the presentation are, from left, Westlake employees Joe Andrepont, principal-community and governmental affairs; Todd Honeycutt, senior plant manager, Lake Charles North and South; Gerry Brooks, senior plant manager polyethylene and olefins; Curtis Brescher, senior director, manufacturing and operations; Laura Bowers, McNeese Foundation executive director; Dr. Srinivasan Ambatipati, McNeese department head of engineering and computer science; and Jennifer Leger, McNeese Foundation director of operations."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Grace%20(1).png", // Placeholder - update with actual URL
                Description = "The W.R. Grace Foundation has donated $9,500 to the McNeese State University Foundation for the engineering endowment within the College of Engineering and Sciences. On hand for the presentation are, from left, Dr. Nikos Kiritsis, McNeese College of Engineering and Sciences dean; GRACE employees: Mark Louviere, training coordinator; Greg Gray, plant manager; Colleen Shepherd, senior administrative assistant; Julie Morris, environmental health and safety manager; Keith Liles, information technology manager; Santana Badon, senior human resources generalist; Travis Griffin, site operations director; and Dr. Srinivasan Ambatipati, McNeese Engineering and Computer Science department head."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/WestlakeCorporation2.png", // Placeholder - update with actual URL
                Description = "Westlake Corporation has donated $5,000 to the McNeese State University Foundation for the McNeese Chapter of the American Institute of Chemical Engineers (AIChE) in support of the ChemE Cube Team. On hand for the presentation are, from left, AIChE member Jeremy Babineaux, Dr. Srinivasan Ambatipati, McNeese department head of engineering and computer science; Kaden Walker, AIChE member; Dr. Ramalingam Subramaniam, McNeese associate professor of chemical engineering; Joe Andrepont, Westlake principal-community and governmental affairs; AIChE members Carson Plaisance, Carson Black, Chaz Castille, Shelby Wallace and Grace Davis; and Dr. Nikos Kiritsis, McNeese College of Engineering and Sciences dean."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/CETF.png", // Placeholder - update with actual URL
                Description = "The Contractors' Educational Trust Fund (CETF) has donated $50,000 to the McNeese State University Foundation in support of construction management curriculum and programs. On hand for the presentation are, from left: Dr. Srinivasan Ambatipati, McNeese Department of Engineering and Computer Science department head; Kenneth Naquin, CETF secretary/treasurer; Dr. Nikos Kiritsis, McNeese College of Engineering and Sciences dean; Courtney Fenet, Louisiana State Licensing Board for Contractors member; Michael McDuff, CETF Board of Trustees member; and David Landreneau, Louisiana Associated General Contractors southwest regional manager."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Schweitzer.jpg", // Placeholder - update with actual URL
                Description = "Tommie and Jeffery Schweitzer have donated $20,000 to the McNeese State University Foundation for the Schweitzer Environmental Engineering Scholarship, adding to a previous donation to this scholarship. Jeffery graduated from McNeese in 1975 with a Bachelor of Science degree in environmental science, and this fund will aid students pursuing a minor in environmental engineering which is a new offering within the department of engineering and computer science beginning this fall. On hand for the presentation are, from left, Dr. Srinivasan Ambatipati, McNeese department head of engineering and computer science, and Jeffery Schweitzer."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Cheniere_.png", // Placeholder - update with actual URL
                Description = "Cheniere Energy has donated $64,950 to the McNeese State University Foundation for several initiatives including the Cheniere Energy Scholarship, H.C. Drew Center for Business and Economic Analysis; McNeese Automotive Engineering Club, McNeese Cowboy Energy Club, and the McNeese Department of Engineering and Computer Science. On hand for the presentation are, from left, Morgan Turpin, McNeese College of Business interim dean; Dr. Kay Zekany, McNeese College of Business associate professor; Dr. Charles Stewart, McNeese College of Science, Engineering and Mathematics dean; Stephanie Huck, Cheniere Energy representative for local government and community affairs; and Dr. Srinivasan Ambatipati, McNeese Engineering and Computer Science department head."
            },
            new SponsorDonorImage
            {
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/sponsors/Commonwealth_LNG%20.jpg", // Placeholder - update with actual URL
                Description = "Commonwealth LNG has donated $205,000 to the McNeese State University Foundation for the Commonwealth LNG Scholarship. The scholarship is designated for engineering or business majors with priority consideration given to Cameron Parish High School graduates or a Cameron Parish resident. On hand for the presentation are, from left, Lyle Hanna, Commonwealth LNG vice president for corporate communications; Jamie Gray, Commonwealth LNG chief operating officer; Laura Bowers, McNeese Foundation executive director; and Jennifer Leger, McNeese Foundation director of operations."
            }
        };
    }
}

