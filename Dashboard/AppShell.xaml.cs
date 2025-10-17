namespace Dashboard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // A global route alias for home navigation
            Routing.RegisterRoute("//Home", typeof(Pages.HomePage));
            
            // Register E-Week year routes (not shown in hamburger menu)
            Routing.RegisterRoute("//EWeek2025", typeof(Pages.EWeek2025Page));
            Routing.RegisterRoute("//EWeek2024", typeof(Pages.EWeek2024Page));
        }
    }
}
