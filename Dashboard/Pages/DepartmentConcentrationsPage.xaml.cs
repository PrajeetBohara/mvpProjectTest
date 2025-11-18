// Code written for Department Concentrations Page functionality
using Dashboard.Models;
using Dashboard.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the DepartmentConcentrationsPage xaml file.
/// Displays concentrations for a selected department.
/// </summary>
[QueryProperty(nameof(Department), "department")]
public partial class DepartmentConcentrationsPage : ContentPage
{
    private readonly AcademicProgramService _programService;
    private string _department = string.Empty;
    public ObservableCollection<AcademicProgram> Concentrations { get; set; } = new();

    public DepartmentConcentrationsPage(AcademicProgramService programService)
    {
        InitializeComponent();
        _programService = programService;
        BindingContext = this;
    }

    /// <summary>
    /// Gets or sets the department name from query parameters.
    /// </summary>
    public string Department
    {
        get => _department;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _department = Uri.UnescapeDataString(value);
            }
            else
            {
                _department = value ?? string.Empty;
            }
            LoadConcentrations();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (string.IsNullOrEmpty(_department))
        {
            LoadConcentrations();
        }
    }

    /// <summary>
    /// Loads concentrations for the selected department.
    /// </summary>
    private void LoadConcentrations()
    {
        if (string.IsNullOrEmpty(_department))
        {
            // Try to get from Shell query as fallback
            if (Shell.Current?.CurrentState?.Location is not null)
            {
                var queryString = Shell.Current.CurrentState.Location.Query;
                if (!string.IsNullOrEmpty(queryString))
                {
                    var parts = queryString.TrimStart('?').Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2 && keyValue[0] == "department")
                        {
                            _department = Uri.UnescapeDataString(keyValue[1]);
                            break;
                        }
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(_department))
        {
            DepartmentNameLabel.Text = _department;
            
            // Load programs for this department
            var programs = _programService.GetProgramsByDepartment(_department);
            Concentrations.Clear();
            foreach (var program in programs)
            {
                Concentrations.Add(program);
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Warning: Department name is empty, cannot load concentrations.");
        }
    }

    /// <summary>
    /// Handles concentration tap to navigate to program details.
    /// </summary>
    private async void OnConcentrationTapped(object? sender, TappedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("=== OnConcentrationTapped triggered ===");
        
        if (sender is Frame frame && frame.BindingContext is AcademicProgram selectedProgram)
        {
            System.Diagnostics.Debug.WriteLine($"Tapped program: {selectedProgram.Name} (ID: {selectedProgram.Id})");
            System.Diagnostics.Debug.WriteLine($"Program has {selectedProgram.ImageUrls?.Count ?? 0} images");
            
            try
            {
                // Navigate to program detail page using query string (same pattern as StudentClubsPage)
                var route = $"//ProgramDetail?programId={selectedProgram.Id}";
                System.Diagnostics.Debug.WriteLine($"Navigating to: {route}");
                
                await Shell.Current.GoToAsync(route);
                
                System.Diagnostics.Debug.WriteLine("✓ Navigation completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Navigation error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                await DisplayAlert("Navigation Error", $"Could not open program details.\n\nError: {ex.Message}", "OK");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("✗ Tapped item is not an AcademicProgram or BindingContext is null");
            if (sender != null)
            {
                System.Diagnostics.Debug.WriteLine($"Sender type: {sender.GetType().Name}");
            }
        }
    }
    
    /// <summary>
    /// Handles concentration selection to navigate to program details (kept for backward compatibility).
    /// </summary>
    private async void OnConcentrationSelected(object? sender, SelectionChangedEventArgs e)
    {
        // This method is kept but not used - we use OnConcentrationTapped instead
        System.Diagnostics.Debug.WriteLine("OnConcentrationSelected called (should use OnConcentrationTapped)");
    }

    /// <summary>
    /// Handles back button click to navigate back to catalogue.
    /// </summary>
    private async void OnBackButtonClicked(object? sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//AcademicCatalogue");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating back to catalogue: {ex.Message}");
        }
    }
}

