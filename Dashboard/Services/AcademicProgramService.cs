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
            // Bachelor of Science in Engineering - Civil Engineering Concentration
            new AcademicProgram
            {
                Id = 1,
                Name = "Civil Engineering Concentration",
                FullName = "Engineering, Civil Engineering Concentration, BS",
                Department = "Bachelor of Science in Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on the design, construction, and maintenance of infrastructure.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59149"
            },
            // Bachelor of Science in Engineering - Computer Engineering Concentration
            new AcademicProgram
            {
                Id = 2,
                Name = "Computer Engineering Concentration",
                FullName = "Engineering, Computer Engineering Concentration, BS",
                Department = "Bachelor of Science in Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program combining computer science and electrical engineering principles.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59287"
            },
            // Bachelor of Science in Engineering - Electrical Engineering Concentration
            new AcademicProgram
            {
                Id = 3,
                Name = "Electrical Engineering Concentration",
                FullName = "Engineering, Electrical Engineering Concentration, BS",
                Department = "Bachelor of Science in Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on electrical systems, electronics, and power generation.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59150"
            },
            // Bachelor of Science in Chemical Engineering
            new AcademicProgram
            {
                Id = 4,
                Name = "Chemical Engineering",
                FullName = "Chemical Engineering, BSChE",
                Department = "Bachelor of Science in Chemical Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on chemical processes, materials, and industrial applications.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59323"
            },
            // Bachelor of Science in Computer Science - General CS Concentration
            new AcademicProgram
            {
                Id = 5,
                Name = "General Computer Science Concentration",
                FullName = "Computer Science, General Computer Science Concentration, BS",
                Department = "Bachelor of Science in Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A comprehensive computer science program covering fundamental concepts in programming, algorithms, and software engineering.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59024"
            },
            // Bachelor of Science in Computer Science - Software Engineering Concentration
            new AcademicProgram
            {
                Id = 6,
                Name = "Software Engineering Concentration",
                FullName = "Computer Science, Software Engineering Concentration, BS",
                Department = "Bachelor of Science in Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on software development, design patterns, and software engineering principles.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59137"
            },
            // Bachelor of Science in Computer Science - Cybersecurity Concentration
            new AcademicProgram
            {
                Id = 7,
                Name = "Cybersecurity Concentration",
                FullName = "Computer Science, Cybersecurity Concentration, BS",
                Department = "Bachelor of Science in Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A specialized program focusing on cybersecurity, network security, and information protection.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59415"
            },
            // Bachelor of Science in Computer Science - AI Concentration
            new AcademicProgram
            {
                Id = 8,
                Name = "Artificial Intelligence Concentration",
                FullName = "Computer Science, Artificial Intelligence Concentration, BS",
                Department = "Bachelor of Science in Computer Science",
                DegreeType = "Bachelor's Degrees",
                Description = "A cutting-edge program focusing on artificial intelligence, machine learning, and intelligent systems.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59423"
            },
            // Bachelor of Science in Mechanical Engineering
            new AcademicProgram
            {
                Id = 9,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Engineering, BSME",
                Department = "Bachelor of Science in Mechanical Engineering",
                DegreeType = "Bachelor's Degrees",
                Description = "A program focused on mechanical systems, design, and manufacturing.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59290"
            },

            // ============================================
            // MINORS
            // ============================================
            new AcademicProgram
            {
                Id = 10,
                Name = "Automation Engineering",
                FullName = "Automation Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in automation and control systems.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59307"
            },
            new AcademicProgram
            {
                Id = 11,
                Name = "Chemical Engineering",
                FullName = "Chemical Engineering, Minor",
                Department = "Chemical Engineering",
                DegreeType = "Minors",
                Description = "A minor program in chemical engineering principles.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59016"
            },
            new AcademicProgram
            {
                Id = 12,
                Name = "Civil Engineering",
                FullName = "Civil Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in civil engineering fundamentals.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59388"
            },
            new AcademicProgram
            {
                Id = 13,
                Name = "Computer Engineering",
                FullName = "Computer Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in computer engineering.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59261"
            },
            new AcademicProgram
            {
                Id = 14,
                Name = "Computer Science",
                FullName = "Computer Science, Minor",
                Department = "Computer Science",
                DegreeType = "Minors",
                Description = "A minor program in computer science fundamentals.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59025"
            },
            new AcademicProgram
            {
                Id = 15,
                Name = "Cybersecurity",
                FullName = "Cybersecurity, Minor",
                Department = "Computer Science",
                DegreeType = "Minors",
                Description = "A minor program in cybersecurity and information protection.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59389"
            },
            new AcademicProgram
            {
                Id = 16,
                Name = "Electrical Engineering",
                FullName = "Electrical Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in electrical engineering principles.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59119"
            },
            new AcademicProgram
            {
                Id = 17,
                Name = "Environmental Engineering",
                FullName = "Environmental Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in environmental engineering and sustainability.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59417"
            },
            new AcademicProgram
            {
                Id = 18,
                Name = "Land Surveying",
                FullName = "Land Surveying, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in land surveying and geomatics.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59416"
            },
            new AcademicProgram
            {
                Id = 19,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Engineering, Minor",
                Department = "Mechanical Engineering",
                DegreeType = "Minors",
                Description = "A minor program in mechanical engineering fundamentals.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59262"
            },
            new AcademicProgram
            {
                Id = 20,
                Name = "Power Engineering",
                FullName = "Power Engineering, Minor",
                Department = "Engineering",
                DegreeType = "Minors",
                Description = "A minor program in power systems and electrical power engineering.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59263"
            },

            // ============================================
            // MASTER'S DEGREES (MEng)
            // ============================================
            new AcademicProgram
            {
                Id = 21,
                Name = "Chemical Engineering Concentration",
                FullName = "Engineering, Chemical Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in chemical engineering with advanced coursework and research.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59152"
            },
            new AcademicProgram
            {
                Id = 22,
                Name = "Civil Engineering Concentration",
                FullName = "Engineering, Civil Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in civil engineering with advanced coursework and research.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59153"
            },
            new AcademicProgram
            {
                Id = 23,
                Name = "Computer Engineering Concentration",
                FullName = "Engineering, Computer Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in computer engineering with advanced coursework and research.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59390"
            },
            new AcademicProgram
            {
                Id = 24,
                Name = "Electrical Engineering Concentration",
                FullName = "Engineering, Electrical Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in electrical engineering with advanced coursework and research.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59154"
            },
            new AcademicProgram
            {
                Id = 25,
                Name = "Mechanical Engineering Concentration",
                FullName = "Engineering, Mechanical Engineering Concentration, MEng",
                Department = "Engineering",
                DegreeType = "Master's Degrees",
                Description = "A master's program in mechanical engineering with advanced coursework and research.",
                Url = "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59155"
            }
        };
    }
}
