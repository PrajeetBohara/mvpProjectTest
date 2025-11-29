// Code written for AcademicProgramService to manage academic program data
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for managing academic program information.
/// </summary>
public class AcademicProgramService
{
    private readonly List<AcademicProgram> _programs;

    /// <summary>
    /// Initializes the AcademicProgramService with program data.
    /// </summary>
    public AcademicProgramService()
    {
        _programs = InitializePrograms();
    }

    /// <summary>
    /// Gets all available academic programs.
    /// </summary>
    /// <returns>List of all academic programs.</returns>
    public List<AcademicProgram> GetAllPrograms()
    {
        return _programs;
    }

    /// <summary>
    /// Gets all programs for a specific degree type.
    /// </summary>
    /// <param name="degreeType">The degree type (e.g., "BS", "Minor", "MEng").</param>
    /// <returns>List of programs for the specified degree type.</returns>
    public List<AcademicProgram> GetProgramsByDegreeType(string degreeType)
    {
        return _programs.Where(p => p.DegreeType.Equals(degreeType, StringComparison.OrdinalIgnoreCase))
                       .OrderBy(p => p.Name)
                       .ToList();
    }

    /// <summary>
    /// Gets all unique degree types.
    /// </summary>
    /// <returns>List of degree type names.</returns>
    public List<string> GetDegreeTypes()
    {
        return _programs.Select(p => p.DegreeType).Distinct().OrderBy(d => d).ToList();
    }

    /// <summary>
    /// Gets a specific program by ID.
    /// </summary>
    /// <param name="id">The program ID.</param>
    /// <returns>The program if found, otherwise null.</returns>
    public AcademicProgram? GetProgramById(int id)
    {
        return _programs.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Initializes the list of academic programs based on the official catalogue.
    /// </summary>
    private List<AcademicProgram> InitializePrograms()
    {
        return new List<AcademicProgram>
        {
            // ============================================
            // BACHELOR OF SCIENCE (BS) PROGRAMS
            // ============================================
            new AcademicProgram
            {
                Id = 1,
                Name = "Applied Computer Science Concentration",
                FullName = "Computer Science, Applied Computer Science Concentration, BS",
                Department = "Computer Science",
                DegreeType = "BS",
                Description = "A program emphasizing practical applications of computer science in real-world scenarios and industries.",
                Url = "https://www.mcneese.edu/academics/computer-science/applied-cs" // Placeholder URL - update with actual URL
            },
            new AcademicProgram
            {
                Id = 2,
                Name = "Artificial Intelligence Concentration",
                FullName = "Computer Science, Artificial Intelligence Concentration, BS",
                Department = "Computer Science",
                DegreeType = "BS",
                Description = "A cutting-edge program focusing on artificial intelligence, machine learning, and intelligent systems.",
                Url = "https://www.mcneese.edu/academics/computer-science/ai-cs" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 3,
                Name = "Cybersecurity Concentration",
                FullName = "Computer Science, Cybersecurity Concentration, BS",
                Department = "Computer Science",
                DegreeType = "BS",
                Description = "A specialized program focusing on cybersecurity, network security, and information protection.",
                Url = "https://www.mcneese.edu/academics/computer-science/cybersecurity-cs" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 4,
                Name = "General Computer Science Concentration",
                FullName = "Computer Science, General Computer Science Concentration, BS",
                Department = "Computer Science",
                DegreeType = "BS",
                Description = "A comprehensive computer science program covering fundamental concepts in programming, algorithms, and software engineering.",
                Url = "https://www.mcneese.edu/academics/computer-science/general-cs" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 5,
                Name = "Industrial Computer Science Concentration",
                FullName = "Computer Science, Industrial Computer Science Concentration, BS",
                Department = "Computer Science",
                DegreeType = "BS",
                Description = "A program focused on applying computer science principles to industrial and manufacturing environments.",
                Url = "https://www.mcneese.edu/academics/computer-science/industrial-cs" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 6,
                Name = "Civil Engineering Concentration",
                FullName = "Engineering, Civil Engineering Concentration, BS",
                Department = "Engineering",
                DegreeType = "BS",
                Description = "A program focused on the design, construction, and maintenance of infrastructure.",
                Url = "https://www.mcneese.edu/academics/engineering/civil-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 7,
                Name = "Computer Engineering Concentration",
                FullName = "Engineering, Computer Engineering Concentration, BS",
                Department = "Engineering",
                DegreeType = "BS",
                Description = "A program combining computer science and electrical engineering principles.",
                Url = "https://www.mcneese.edu/academics/engineering/computer-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 8,
                Name = "Electrical Engineering Concentration",
                FullName = "Engineering, Electrical Engineering Concentration, BS",
                Department = "Engineering",
                DegreeType = "BS",
                Description = "A program focused on electrical systems, electronics, and power generation.",
                Url = "https://www.mcneese.edu/academics/engineering/electrical-engineering" // Placeholder URL
            },

            // ============================================
            // BACHELOR OF SCIENCE IN CHEMICAL ENGINEERING (BSChE)
            // ============================================
            new AcademicProgram
            {
                Id = 9,
                Name = "Chemical Engineering",
                FullName = "Chemical Engineering, BSChE",
                Department = "Chemical Engineering",
                DegreeType = "BSChE",
                Description = "A program focused on chemical processes, materials, and industrial applications.",
                Url = "https://www.mcneese.edu/academics/chemical-engineering" // Placeholder URL
            },

            // ============================================
            // BACHELOR OF SCIENCE IN MECHANICAL ENGINEERING (BSME)
            // ============================================
            new AcademicProgram
            {
                Id = 10,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Engineering, BSME",
                Department = "Mechanical Engineering",
                DegreeType = "BSME",
                Description = "A program focused on mechanical systems, design, and manufacturing.",
                Url = "https://www.mcneese.edu/academics/mechanical-engineering" // Placeholder URL
            },

            // ============================================
            // DUAL DEGREE PROGRAMS
            // ============================================
            new AcademicProgram
            {
                Id = 11,
                Name = "BS/MS Computer Science",
                FullName = "Computer Science, General Computer Science Concentration, BS/Mathematical Sciences, Computer Science Concentration, MS",
                Department = "Computer Science",
                DegreeType = "Dual Degree",
                Description = "An accelerated program combining bachelor's and master's degrees in computer science.",
                Url = "https://www.mcneese.edu/academics/computer-science/dual-degree" // Placeholder URL
            },

            // ============================================
            // MINORS
            // ============================================
            new AcademicProgram
            {
                Id = 12,
                Name = "Automation Engineering",
                FullName = "Automation Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in automation and control systems.",
                Url = "https://www.mcneese.edu/academics/minors/automation-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 13,
                Name = "Chemical Engineering",
                FullName = "Chemical Engineering, Minor",
                Department = "Chemical Engineering",
                DegreeType = "Minor",
                Description = "A minor program in chemical engineering principles.",
                Url = "https://www.mcneese.edu/academics/minors/chemical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 14,
                Name = "Civil Engineering",
                FullName = "Civil Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in civil engineering fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/civil-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 15,
                Name = "Computer Engineering",
                FullName = "Computer Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in computer engineering.",
                Url = "https://www.mcneese.edu/academics/minors/computer-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 16,
                Name = "Computer Science",
                FullName = "Computer Science, Minor",
                Department = "Computer Science",
                DegreeType = "Minor",
                Description = "A minor program in computer science fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/computer-science" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 17,
                Name = "Cybersecurity",
                FullName = "Cybersecurity, Minor",
                Department = "Computer Science",
                DegreeType = "Minor",
                Description = "A minor program in cybersecurity and information protection.",
                Url = "https://www.mcneese.edu/academics/minors/cybersecurity" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 18,
                Name = "Electrical Engineering",
                FullName = "Electrical Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in electrical engineering principles.",
                Url = "https://www.mcneese.edu/academics/minors/electrical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 19,
                Name = "Environmental Engineering",
                FullName = "Environmental Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in environmental engineering and sustainability.",
                Url = "https://www.mcneese.edu/academics/minors/environmental-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 20,
                Name = "Land Surveying",
                FullName = "Land Surveying, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in land surveying and geomatics.",
                Url = "https://www.mcneese.edu/academics/minors/land-surveying" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 21,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Engineering, Minor",
                Department = "Mechanical Engineering",
                DegreeType = "Minor",
                Description = "A minor program in mechanical engineering fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/mechanical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 22,
                Name = "Power Engineering",
                FullName = "Power Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minor",
                Description = "A minor program in power systems and electrical power engineering.",
                Url = "https://www.mcneese.edu/academics/minors/power-engineering" // Placeholder URL
            },

            // ============================================
            // MASTER OF ENGINEERING (MEng) PROGRAMS
            // ============================================
            new AcademicProgram
            {
                Id = 23,
                Name = "Chemical Engineering Concentration",
                FullName = "Engineering, Chemical Engineering Concentration, MEng",
                Department = "Chemical Engineering",
                DegreeType = "MEng",
                Description = "A master's program in chemical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/chemical-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 24,
                Name = "Civil Engineering Concentration",
                FullName = "Engineering, Civil Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "MEng",
                Description = "A master's program in civil engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/civil-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 25,
                Name = "Computer Engineering Concentration",
                FullName = "Engineering, Computer Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "MEng",
                Description = "A master's program in computer engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/computer-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 26,
                Name = "Electrical Engineering Concentration",
                FullName = "Engineering, Electrical Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "MEng",
                Description = "A master's program in electrical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/electrical-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 27,
                Name = "Mechanical Engineering Concentration",
                FullName = "Engineering, Mechanical Engineering Concentration, MEng",
                Department = "Mechanical Engineering",
                DegreeType = "MEng",
                Description = "A master's program in mechanical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/mechanical-engineering-meng" // Placeholder URL
            }
        };
    }
}
