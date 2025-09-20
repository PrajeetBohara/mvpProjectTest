using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace Dashboard.Controls
{
    /// <summary>
    /// TopBar control for Smart TV Kiosk Dashboard.
    /// Provides global navigation and information display that is always visible.
    /// Layout: [Hamburger Menu] → [Home Button] → [Department Title] → [Search] → [Weather] → [Clock]
    /// The hamburger menu contains: Home, Student Clubs, Faculty Directory, etc.
    /// </summary>
    public partial class TopBar : ContentView
    {
        #region Private Fields
        private IDispatcherTimer? _clockTimer;
        private IDispatcherTimer? _weatherTimer;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the TopBar control.
        /// Sets up timers for clock and weather updates.
        /// </summary>
        public TopBar()
        {
            InitializeComponent();
            
            // Use Loaded event to ensure XAML is fully loaded before starting timers
            this.Loaded += OnTopBarLoaded;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the Loaded event to start timers after XAML is fully loaded.
        /// This prevents null reference exceptions when accessing UI elements.
        /// </summary>
        private void OnTopBarLoaded(object? sender, EventArgs e)
        {
            StartClock();
            StartWeatherUpdates();
            UpdateInitialDisplay();
        }

        /// <summary>
        /// Handles the hamburger menu tap to open the Shell flyout.
        /// For Smart TV Kiosk: Opens side menu with navigation options:
        /// - Home
        /// - Student Clubs  
        /// - Faculty Directory
        /// - Other dashboard sections
        /// </summary>
        private void OnMenuTapped(object sender, EventArgs e)
        {
            try
            {
                if (Shell.Current != null)
                {
                    Shell.Current.FlyoutIsPresented = true;
                }
            }
            catch (Exception ex)
            {
                // Log error in production
                System.Diagnostics.Debug.WriteLine($"Error opening flyout: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the home button tap to navigate to the home page.
        /// Uses Shell navigation to ensure proper routing.
        /// </summary>
        private async void OnHomeTapped(object sender, EventArgs e)
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//Home");
                }
            }
            catch (Exception ex)
            {
                // Log error in production
                System.Diagnostics.Debug.WriteLine($"Error navigating to home: {ex.Message}");
            }
        }
        #endregion

        #region Timer Management
        /// <summary>
        /// Starts the clock timer to update time and date every second.
        /// Uses IDispatcherTimer for UI thread safety.
        /// </summary>
        private void StartClock()
        {
            try
            {
                _clockTimer = Dispatcher.CreateTimer();
                _clockTimer.Interval = TimeSpan.FromSeconds(1);
                _clockTimer.IsRepeating = true;
                _clockTimer.Tick += OnClockTick;
                _clockTimer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error starting clock timer: {ex.Message}");
            }
        }

        /// <summary>
        /// Starts the weather update timer (placeholder for future weather API integration).
        /// Currently updates every 5 minutes as a placeholder.
        /// </summary>
        private void StartWeatherUpdates()
        {
            try
            {
                _weatherTimer = Dispatcher.CreateTimer();
                _weatherTimer.Interval = TimeSpan.FromMinutes(5); // Update every 5 minutes
                _weatherTimer.IsRepeating = true;
                _weatherTimer.Tick += OnWeatherTick;
                _weatherTimer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error starting weather timer: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles clock timer tick to update time and date display.
        /// </summary>
        private void OnClockTick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        /// <summary>
        /// Handles weather timer tick (placeholder for future weather API calls).
        /// </summary>
        private void OnWeatherTick(object? sender, EventArgs e)
        {
            UpdateWeather();
        }
        #endregion

        #region Display Updates
        /// <summary>
        /// Updates the initial display values when the control is first loaded.
        /// </summary>
        private void UpdateInitialDisplay()
        {
            UpdateClock();
            UpdateWeather();
        }

        /// <summary>
        /// Updates the clock display with current time and date in single line format.
        /// Uses safe null checking to prevent crashes.
        /// </summary>
        private void UpdateClock()
        {
            try
            {
                var now = DateTime.Now;
                
                // Update single-line clock display
                if (ClockLabel != null)
                {
                    ClockLabel.Text = $"{now:hh:mm tt} • {now:MMM dd}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating clock: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the weather display in single line format.
        /// Currently shows static data, but ready for weather API integration.
        /// </summary>
        private void UpdateWeather()
        {
            try
            {
                // Placeholder weather data - in production, this would call a weather API
                if (WeatherLabel != null)
                {
                    WeatherLabel.Text = "72°F • Partly Cloudy";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating weather: {ex.Message}");
            }
        }
        #endregion

        #region Lifecycle Management
        /// <summary>
        /// Cleans up timers when the control is removed from the visual tree.
        /// This prevents memory leaks and unnecessary timer execution.
        /// </summary>
        protected override void OnParentSet()
        {
            base.OnParentSet();
            
            if (Parent == null)
            {
                // Clean up timers when control is removed
                _clockTimer?.Stop();
                _clockTimer = null;
                
                _weatherTimer?.Stop();
                _weatherTimer = null;
            }
        }

        /// <summary>
        /// Additional cleanup when the control is disposed.
        /// </summary>
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            if (Handler == null)
            {
                // Clean up when handler is removed
                _clockTimer?.Stop();
                _clockTimer = null;
                
                _weatherTimer?.Stop();
                _weatherTimer = null;
            }
        }
        #endregion
    }
}
