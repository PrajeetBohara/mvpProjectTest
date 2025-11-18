// Code written for Academic Catalogue Page functionality
using System;
using System.Collections.Generic;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the AcademicCataloguePage xaml file.
/// Displays department selection cards.
/// </summary>
public partial class AcademicCataloguePage : ContentPage
{
    public AcademicCataloguePage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles Computer Science department tap to navigate to concentrations.
    /// </summary>
    private async void OnComputerScienceTapped(object? sender, EventArgs e)
    {
        try
        {
            if (Shell.Current == null)
            {
                await DisplayAlert("Error", "Shell.Current is null", "OK");
                return;
            }

            // Use Dictionary for navigation parameters (more reliable than query strings)
            var parameters = new Dictionary<string, object>
            {
                { "department", "Computer Science" }
            };

            System.Diagnostics.Debug.WriteLine($"Attempting navigation to DepartmentConcentrations with department: Computer Science");
            
            await Shell.Current.GoToAsync("//DepartmentConcentrations", parameters);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating to Computer Science concentrations: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
            await DisplayAlert("Navigation Error", $"Could not navigate to Computer Science concentrations.\n\nError: {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// Handles Chemical Engineering department tap to navigate to concentrations.
    /// </summary>
    private async void OnChemicalEngineeringTapped(object? sender, EventArgs e)
    {
        try
        {
            if (Shell.Current == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "department", "Chemical Engineering" }
            };

            await Shell.Current.GoToAsync("//DepartmentConcentrations", parameters);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating to Chemical Engineering concentrations: {ex.Message}");
            await DisplayAlert("Error", "Could not navigate to Chemical Engineering concentrations.", "OK");
        }
    }

    /// <summary>
    /// Handles Mechanical Engineering department tap to navigate to concentrations.
    /// </summary>
    private async void OnMechanicalEngineeringTapped(object? sender, EventArgs e)
    {
        try
        {
            if (Shell.Current == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "department", "Mechanical Engineering" }
            };

            await Shell.Current.GoToAsync("//DepartmentConcentrations", parameters);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating to Mechanical Engineering concentrations: {ex.Message}");
            await DisplayAlert("Error", "Could not navigate to Mechanical Engineering concentrations.", "OK");
        }
    }
}

