using System.Collections.ObjectModel;
using Dashboard.Models;
using Dashboard.Services;

namespace Dashboard.Pages;

public partial class ContactPage : ContentPage
{
    private readonly FacultyService _facultyService;
    public ObservableCollection<Faculty> DepartmentHeads { get; set; } = new();
    public ObservableCollection<Faculty> KeyStaff { get; set; } = new();

    public ContactPage()
    {
        InitializeComponent();
        _facultyService = Application.Current!.Handler!.MauiContext!.Services.GetService<FacultyService>()!;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFacultyData();
    }

    private async Task LoadFacultyData()
    {
        try
        {
            var connectionOk = await _facultyService.TestConnectionAsync();
            System.Diagnostics.Debug.WriteLine($"Faculty database connection test: {connectionOk}");

            // Load department heads
            var departmentHeads = await _facultyService.GetDepartmentHeadsAsync();
            System.Diagnostics.Debug.WriteLine($"Loaded {departmentHeads.Count} department heads");

            DepartmentHeads.Clear();
            foreach (var faculty in departmentHeads)
            {
                DepartmentHeads.Add(faculty);
            }

            // Load key staff
            var keyStaff = await _facultyService.GetKeyStaffAsync();
            System.Diagnostics.Debug.WriteLine($"Loaded {keyStaff.Count} key staff members");

            KeyStaff.Clear();
            foreach (var faculty in keyStaff)
            {
                KeyStaff.Add(faculty);
            }

            // Add fallback data if no faculty found
            // Here the AvatarUrl fetches images from Supabase storage
            if (DepartmentHeads.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No department heads found, adding fallback data");
                DepartmentHeads.Add(new Faculty
                {
                    Id = Guid.NewGuid(),
                    Title = "Eweek Lead",
                    FullName = "Dr. Firouz Rosti",
                    Email = "xyz@mcneese.edu",
                    Phone = "(337) 475-5850",
                    AvatarUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty/Firouz%20Rosti.avif",
                    OfficeLocation = "Drew Hall, Room 105",
                    Department = "Engineering & Computer Science",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                DepartmentHeads.Add(new Faculty
                {
                    Id = Guid.NewGuid(),
                    Title = "Department Head",
                    FullName = "Dr. Srinivasan Ambatipati",
                    Email = "xyz@mcneese.edu",
                    Phone = "(337) 475-5850",
                    AvatarUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty/1701392170517.jpg",
                    OfficeLocation = "Drew Hall, Room 105",
                    Department = "Engineering & Computer Science",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }

            if (KeyStaff.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No key staff found, adding fallback data");
                KeyStaff.Add(new Faculty
                {
                    Id = Guid.NewGuid(),
                    Title = "Assistant Professor",
                    FullName = "Dr. Jennifer Lavergne",
                    Email = "jlavegne@mcneese.edu",
                    Phone = "(337) 475-5852",
                    AvatarUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty/1681218400526.jpg",
                    OfficeLocation = "Drew Hall, Room 201",
                    Department = "Computer Science",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading faculty data: {ex.Message}");
        }
    }

}
