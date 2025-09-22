//Code written by Prajeet Bohara

using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the HomePage xaml file.
/// </summary>
public partial class HomePage : ContentPage
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the HomePage.
    /// Sets up the main dashboard with all sections.
    /// </summary>
    public HomePage()
    {
        InitializeComponent();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles slideshow tap to open image gallery.
    /// Later on for Smart it will open full-screen image slideshow.
    /// </summary>
    private async void OnSlideshowTapped(object sender, EventArgs e)
    {
        try
        {
            await DisplayAlert("Slideshow", "Opening image gallery...", "OK");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening slideshow: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles "View All Announcements" button tap.
    /// On tapping view all, it will route to the all announcements page.
    /// </summary>
    private async void OnViewAllAnnouncementsClicked(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//AllAnnouncements");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening announcements: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles "View All Events" button tap.
    /// On tapping view all, it will route to the all events page.
    /// </summary>
    private async void OnViewAllEventsClicked(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//AllEvents");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening events: {ex.Message}");
        }
    }

            /// <summary>
            /// Handles maps section tap to open maps for Drew Hall and ETL.
            /// Placeholder for now
            /// </summary>
            private async void OnMapsTapped(object sender, EventArgs e)
            {
                try
                {
                    await DisplayAlert("Campus Maps", "Opening interactive campus maps...", "OK");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error opening maps: {ex.Message}");
                }
            }

            ///// <summary>
            ///// Handles stats section tap to open detailed statistics.
            ///// We are not using it right now. This is kept for future reference
            ///// </summary>
            //private async void OnStatsTapped(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        // TODO: Navigate to detailed stats page
            //        await DisplayAlert("Statistics & Accreditation", "Opening detailed statistics and accreditation information...", "OK");
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"Error opening stats: {ex.Message}");
            //    }
            //}
    #endregion
}
