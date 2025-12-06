// Code written for LabService to manage lab data
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for managing laboratory information.
/// </summary>
public class LabService
{
    private readonly List<Lab> _labs;

    /// <summary>
    /// Initializes the LabService with lab data.
    /// </summary>
    public LabService()
    {
        _labs = InitializeLabs();
    }

    /// <summary>
    /// Gets all labs.
    /// </summary>
    /// <returns>List of all labs.</returns>
    public List<Lab> GetAllLabs()
    {
        return _labs;
    }

    /// <summary>
    /// Gets all labs for a specific building.
    /// </summary>
    /// <param name="building">The building name (e.g., "Drew Hall", "ETL").</param>
    /// <returns>List of labs in the specified building.</returns>
    public List<Lab> GetLabsByBuilding(string building)
    {
        return _labs.Where(l => l.Building.Equals(building, StringComparison.OrdinalIgnoreCase))
                   .OrderBy(l => l.Location)
                   .ToList();
    }

    /// <summary>
    /// Gets all unique buildings.
    /// </summary>
    /// <returns>List of building names.</returns>
    public List<string> GetBuildings()
    {
        return _labs.Select(l => l.Building).Distinct().OrderBy(b => b).ToList();
    }

    /// <summary>
    /// Initializes the list of labs.
    /// </summary>
    private List<Lab> InitializeLabs()
    {
        return new List<Lab>
        {
            // Drew Hall Labs
            new Lab
            {
                Name = "Digital Systems & Microcontrollers Lab",
                Location = "Drew 327",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "PLC & Control Systems Lab",
                Location = "Drew 328",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Computer/ACM Lab",
                Location = "Drew 317",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Robotics Lab",
                Location = "Drew 304",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Networking Lab",
                Location = "Drew 303",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "VR/Game Lab",
                Location = "Drew 228",
                Building = "Drew Hall",
                Type = LabType.Teaching
            },

            // ETL Labs
            new Lab
            {
                Name = "Electronics Lab",
                Location = "ETL 100 & 101",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Network & Security Lab",
                Location = "ETL 102",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Industrial Process Control Lab",
                Location = "ETL 103 & 106",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Chemical Research Lab",
                Location = "ETL 105",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Material Science/ PC Research Lab",
                Location = "ETL 109",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Construction Materials Lab",
                Location = "ETL 113",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Power Research Lab",
                Location = "ETL 114A",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Construction Materials Research Lab",
                Location = "ETL 115",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Thermal/ Strengths Lab",
                Location = "ETL 116",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Land Surveying Lab",
                Location = "ETL 117",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Soil Research Lab",
                Location = "ETL 118",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "3-D Printing Lab",
                Location = "ETL 119",
                Building = "ETL",
                Type = LabType.Teaching
            },
            new Lab
            {
                Name = "Fluid Research Lab",
                Location = "ETL 120",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Computational Fluid Dynamics Research Lab",
                Location = "ETL 121",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Concrete Research Lab",
                Location = "ETL 122",
                Building = "ETL",
                Type = LabType.Research
            },
            new Lab
            {
                Name = "Sound & Vibrations Research Lab",
                Location = "ETL 123",
                Building = "ETL",
                Type = LabType.Research
            }
        };
    }
}

