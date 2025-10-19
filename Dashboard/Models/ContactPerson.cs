namespace Dashboard.Models;

public class ContactPerson
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string OfficeLocation { get; set; } = string.Empty;
    public bool IsDepartmentHead { get; set; } = false;
    public string Specialization { get; set; } = string.Empty;
}
