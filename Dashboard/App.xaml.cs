namespace Dashboard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Use AppShell directly
            MainPage = new AppShell();
        }
    }
}
