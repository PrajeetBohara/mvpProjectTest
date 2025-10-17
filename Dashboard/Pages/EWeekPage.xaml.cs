using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// EWeekPage for displaying E-Week main page with sections for 2025 and 2024.
/// This page provides navigation to specific year pages.
/// </summary>
public partial class EWeekPage : ContentPage
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the EWeekPage.
    /// </summary>
    public EWeekPage()
    {
        InitializeComponent();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the tap event for E-Week 2025 section.
    /// Navigates to the E-Week 2025 page.
    /// </summary>
    private async void OnEWeek2025Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EWeek2025");
    }

    /// <summary>
    /// Handles the tap event for E-Week 2024 section.
    /// Navigates to the E-Week 2024 page.
    /// </summary>
    private async void OnEWeek2024Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EWeek2024");
    }
    #endregion
}
