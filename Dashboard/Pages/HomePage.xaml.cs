using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// HomePage for the Smart TV Kiosk Dashboard.
/// Features: Slideshow, Upcoming Events, Announcements, Campus Maps, and University Logo.
/// Layout: TopBar (always visible) + Main content with 5 key sections.
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
    /// For Smart TV Kiosk: Opens full-screen image slideshow.
    /// </summary>
    private async void OnSlideshowTapped(object sender, EventArgs e)
    {
        try
        {
            // TODO: Navigate to full-screen slideshow/gallery
            await DisplayAlert("Slideshow", "Opening image gallery...", "OK");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening slideshow: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles "View All Announcements" button tap.
    /// For Smart TV Kiosk: Navigate to dedicated announcements page.
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
    /// For Smart TV Kiosk: Navigate to dedicated events calendar page.
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
            /// Handles maps section tap to open campus maps.
            /// For Smart TV Kiosk: Navigate to interactive campus maps page.
            /// </summary>
            private async void OnMapsTapped(object sender, EventArgs e)
            {
                try
                {
                    // TODO: Navigate to maps page
                    await DisplayAlert("Campus Maps", "Opening interactive campus maps...", "OK");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error opening maps: {ex.Message}");
                }
            }

            /// <summary>
            /// Handles stats section tap to open detailed statistics.
            /// For Smart TV Kiosk: Navigate to detailed stats and accreditation page.
            /// </summary>
            private async void OnStatsTapped(object sender, EventArgs e)
            {
                try
                {
                    // TODO: Navigate to detailed stats page
                    await DisplayAlert("Statistics & Accreditation", "Opening detailed statistics and accreditation information...", "OK");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error opening stats: {ex.Message}");
                }
            }
    #endregion
}
