using System;
using Microsoft.Maui.Controls;

namespace Dashboard.Controls
{
    public partial class TopBar : ContentView
    {
        readonly TimeSpan _clockTick = TimeSpan.FromSeconds(1);

        public TopBar()
        {
            InitializeComponent();
            // start a simple clock
            Device.StartTimer(_clockTick, () =>
            {
                ClockLabel.Text = DateTime.Now.ToString("hh:mm tt");
                return true; // keep ticking
            });
        }

        // Opens the Shell flyout (side menu)
        private void OnMenuTapped(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        // Navigate to the Home page (we'll define Home later; for now it goes to MainPage route)
        private async void OnHomeTapped(object sender, EventArgs e)
        {
            // if we keep MainPage as our home route
            await Shell.Current.GoToAsync("//Home");
        }
    }
}
