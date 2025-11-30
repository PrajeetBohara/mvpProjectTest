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
            // BACHELOR'S DEGREES
            // ============================================
            // B.S in Engineering - Civil Engineering Concentration
            new AcademicProgram
            {
                Id = 1,
                Name = "Civil Engineering Concentration",
                FullName = "Eng. Civil Eng. Concen., B.S",
                Department = "Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on the design, construction, and maintenance of infrastructure.",
                Url = "https://www.mcneese.edu/academics/engineering/civil-engineering" // Placeholder URL - update with actual URL
            },
            // B.S in Engineering - Computer Engineering Concentration
            new AcademicProgram
            {
                Id = 2,
                Name = "Computer Engineering Concentration",
                FullName = "Eng. Comp. Eng. Concen., B.S",
                Department = "Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program combining computer science and electrical engineering principles.",
                Url = "https://www.mcneese.edu/academics/engineering/computer-engineering" // Placeholder URL
            },
            // B.S in Engineering - Electrical Engineering Concentration
            new AcademicProgram
            {
                Id = 3,
                Name = "Electrical Engineering Concentration",
                FullName = "Eng. Electrical Eng. Concen., B.S",
                Department = "Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on electrical systems, electronics, and power generation.",
                Url = "https://www.mcneese.edu/academics/engineering/electrical-engineering" // Placeholder URL
            },
            // B.S in Chemical Engineering
            new AcademicProgram
            {
                Id = 4,
                Name = "Chemical Engineering",
                FullName = "B.S in Chemical Eng.",
                Department = "Chemical Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on chemical processes, materials, and industrial applications.",
                Url = "https://www.mcneese.edu/academics/chemical-engineering" // Placeholder URL
            },
            // B.S in Computer Science - General CS Concentration
            new AcademicProgram
            {
                Id = 5,
                Name = "General CS Concentration",
                FullName = "CS. General CS. Con., B.S",
                Department = "Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A comprehensive computer science program covering fundamental concepts in programming, algorithms, and software engineering.",
                Url = "https://www.mcneese.edu/academics/computer-science/general-cs" // Placeholder URL
            },
            // B.S in Computer Science - Software Engineering Concentration
            new AcademicProgram
            {
                Id = 6,
                Name = "Software Engineering Concentration",
                FullName = "CS. Software Eng. Con., B.S",
                Department = "Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on software development, design patterns, and software engineering principles.",
                Url = "https://www.mcneese.edu/academics/computer-science/software-engineering" // Placeholder URL
            },
            // B.S in Computer Science - Cyber Security Concentration
            new AcademicProgram
            {
                Id = 7,
                Name = "Cyber Security Concentration",
                FullName = "CS. Cyber Security Con., B.S",
                Department = "Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A specialized program focusing on cybersecurity, network security, and information protection.",
                Url = "https://www.mcneese.edu/academics/computer-science/cybersecurity-cs" // Placeholder URL
            },
            // B.S in Computer Science - AI Concentration
            new AcademicProgram
            {
                Id = 8,
                Name = "AI Concentration",
                FullName = "CS.AI concen., B.S",
                Department = "Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A cutting-edge program focusing on artificial intelligence, machine learning, and intelligent systems.",
                Url = "https://www.mcneese.edu/academics/computer-science/ai-cs" // Placeholder URL
            },
            // B.S in Mechanical Engineering
            new AcademicProgram
            {
                Id = 9,
                Name = "Mechanical Engineering",
                FullName = "B.S in Mechanical Eng.",
                Department = "Mechanical Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on mechanical systems, design, and manufacturing.",
                Url = "https://www.mcneese.edu/academics/mechanical-engineering" // Placeholder URL
            },

            // ============================================
            // MINORS
            // ============================================
            new AcademicProgram
            {
                Id = 10,
                Name = "Automation Engineering",
                FullName = "Automation Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in automation and control systems.",
                Url = "https://www.mcneese.edu/academics/minors/automation-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 11,
                Name = "Chemical Engineering",
                FullName = "Chemical Eng., Minor",
                Department = "Chemical Engineering",
                DegreeType = "Minors",
                Description = "A minor program in chemical engineering principles.",
                Url = "https://www.mcneese.edu/academics/minors/chemical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 12,
                Name = "Civil Engineering",
                FullName = "Civil Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in civil engineering fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/civil-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 13,
                Name = "Computer Engineering",
                FullName = "Computer Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in computer engineering.",
                Url = "https://www.mcneese.edu/academics/minors/computer-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 14,
                Name = "Computer Science",
                FullName = "Computer Science, Minor",
                Department = "Computer Science",
                DegreeType = "Minors",
                Description = "A minor program in computer science fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/computer-science" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 15,
                Name = "Cybersecurity",
                FullName = "Cybersecurity, Minor",
                Department = "Computer Science",
                DegreeType = "Minors",
                Description = "A minor program in cybersecurity and information protection.",
                Url = "https://www.mcneese.edu/academics/minors/cybersecurity" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 16,
                Name = "Electrical Engineering",
                FullName = "Electrical Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in electrical engineering principles.",
                Url = "https://www.mcneese.edu/academics/minors/electrical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 17,
                Name = "Environmental Engineering",
                FullName = "Environmental Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in environmental engineering and sustainability.",
                Url = "https://www.mcneese.edu/academics/minors/environmental-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 18,
                Name = "Land Surveying",
                FullName = "Land Surveying, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in land surveying and geomatics.",
                Url = "https://www.mcneese.edu/academics/minors/land-surveying" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 19,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Eng., Minor",
                Department = "Mechanical Engineering",
                DegreeType = "Minors",
                Description = "A minor program in mechanical engineering fundamentals.",
                Url = "https://www.mcneese.edu/academics/minors/mechanical-engineering" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 20,
                Name = "Power Engineering",
                FullName = "Power Eng., Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in power systems and electrical power engineering.",
                Url = "https://www.mcneese.edu/academics/minors/power-engineering" // Placeholder URL
            },

            // ============================================
            // MASTER'S DEGREES (MEng)
            // ============================================
            new AcademicProgram
            {
                Id = 21,
                Name = "Chemical Engineering Concentration",
                FullName = "Eng., Chemical Eng. Con., MEng",
                Department = "Chemical Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in chemical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/chemical-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 22,
                Name = "Civil Engineering Concentration",
                FullName = "Eng., Civil Eng. Con., MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in civil engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/civil-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 23,
                Name = "Computer Engineering Concentration",
                FullName = "Eng., Computer Eng. Con., MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in computer engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/computer-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 24,
                Name = "Electrical Engineering Concentration",
                FullName = "Eng., Electrical Eng. Con., MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in electrical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/electrical-engineering-meng" // Placeholder URL
            },
            new AcademicProgram
            {
                Id = 25,
                Name = "Mechanical Engineering Concentration",
                FullName = "Eng., Mechanical Eng. Con., MEng",
                Department = "Mechanical Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in mechanical engineering with advanced coursework and research.",
                Url = "https://www.mcneese.edu/academics/graduate/mechanical-engineering-meng" // Placeholder URL
            }
        };
    }
}
