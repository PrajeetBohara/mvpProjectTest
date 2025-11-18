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
    /// Gets all programs for a specific department.
    /// </summary>
    /// <param name="department">The department name (e.g., "Computer Science").</param>
    /// <returns>List of programs in the specified department.</returns>
    public List<AcademicProgram> GetProgramsByDepartment(string department)
    {
        return _programs.Where(p => p.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
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
    /// Gets all unique departments.
    /// </summary>
    /// <returns>List of department names.</returns>
    public List<string> GetDepartments()
    {
        return _programs.Select(p => p.Department).Distinct().ToList();
    }

    /// <summary>
    /// Initializes the list of academic programs.
    /// </summary>
    private List<AcademicProgram> InitializePrograms()
    {
        return new List<AcademicProgram>
        {
            // Computer Science Programs
            new AcademicProgram
            {
                Id = 1,
                Name = "Industrial CS",
                FullName = "Industrial Computer Science",
                Department = "Computer Science",
                Description = "A program focused on applying computer science principles to industrial and manufacturing environments.",
                ImageUrls = new List<string> 
                { 
                    "degree_catalogue/industrial/Industrial1.jpg",
                    "degree_catalogue/industrial/Industrial2.jpg",
                    "degree_catalogue/industrial/Industrial3.jpg"
                }
            },
            new AcademicProgram
            {
                Id = 2,
                Name = "General CS",
                FullName = "General Computer Science",
                Department = "Computer Science",
                Description = "A comprehensive computer science program covering fundamental concepts in programming, algorithms, and software engineering.",
                ImageUrls = new List<string> 
                { 
                    "degree_catalogue/general/General1.jpg",
                    "degree_catalogue/general/General2.jpg",
                    "degree_catalogue/general/General3.jpg"
                }
            },
            new AcademicProgram
            {
                Id = 3,
                Name = "Cyber Security CS",
                FullName = "Cyber Security Computer Science",
                Department = "Computer Science",
                Description = "A specialized program focusing on cybersecurity, network security, and information protection.",
                ImageUrls = new List<string> 
                { 
                    "degree_catalogue/cybersecurity/Cyber1.jpg",
                    "degree_catalogue/cybersecurity/Cyber2.jpg",
                    "degree_catalogue/cybersecurity/Cyber3.jpg"
                }
            },
            new AcademicProgram
            {
                Id = 4,
                Name = "Applied CS",
                FullName = "Applied Computer Science",
                Department = "Computer Science",
                Description = "A program emphasizing practical applications of computer science in real-world scenarios and industries.",
                ImageUrls = new List<string> 
                { 
                    "degree_catalogue/applied/Applied1.jpg",
                    "degree_catalogue/applied/Applied2.jpg",
                    "degree_catalogue/applied/Applied3.jpg"
                }
            },
            new AcademicProgram
            {
                Id = 5,
                Name = "Artificial Intelligence CS",
                FullName = "Artificial Intelligence Computer Science",
                Department = "Computer Science",
                Description = "A cutting-edge program focusing on artificial intelligence, machine learning, and intelligent systems.",
                ImageUrls = new List<string> 
                { 
                    "degree_catalogue/ai/AI1.jpg",
                    "degree_catalogue/ai/AI2.jpg",
                    "degree_catalogue/ai/AI3.jpg"
                }
            },
            
            // Chemical Engineering Programs (Placeholder for future)
            new AcademicProgram
            {
                Id = 6,
                Name = "Chemical Engineering",
                FullName = "Chemical Engineering",
                Department = "Chemical Engineering",
                Description = "Chemical Engineering program details.",
                ImageUrls = new List<string> { "mcneeselogo.png" }
            },
            
            // Mechanical Engineering Programs (Placeholder for future)
            new AcademicProgram
            {
                Id = 7,
                Name = "Mechanical Engineering",
                FullName = "Mechanical Engineering",
                Department = "Mechanical Engineering",
                Description = "Mechanical Engineering program details.",
                ImageUrls = new List<string> { "mcneeselogo.png" }
            }
        };
    }
}

