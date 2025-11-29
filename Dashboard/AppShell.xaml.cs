namespace Dashboard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            // Register routes BEFORE InitializeComponent to ensure they're available
            // A global route alias for home navigation
            Routing.RegisterRoute("//Home", typeof(Pages.HomePage));
            
            // Register E-Week year routes (not shown in hamburger menu)
            Routing.RegisterRoute("//EWeek2025", typeof(Pages.EWeek2025Page));
            Routing.RegisterRoute("//EWeek2024", typeof(Pages.EWeek2024Page));
            
            // Register Maps route
            Routing.RegisterRoute("//Maps", typeof(Pages.MapsPage));
            
            // Register Department Map route
            Routing.RegisterRoute("//DepartmentMap", typeof(Pages.DepartmentMapPage));
            
            // Register Campus Map route
            Routing.RegisterRoute("//CampusMap", typeof(Pages.CampusMapPage));
            
            // Register Student Clubs routes
            Routing.RegisterRoute("Clubs", typeof(Pages.StudentClubsPage));
            Routing.RegisterRoute("ClubDetail", typeof(Pages.StudentClubDetailPage));
            
            // Register Academic Catalogue routes
            Routing.RegisterRoute("//AcademicCatalogue", typeof(Pages.AcademicCataloguePage));
            Routing.RegisterRoute("//DepartmentConcentrations", typeof(Pages.DepartmentConcentrationsPage));
            Routing.RegisterRoute("//ProgramDetail", typeof(Pages.AcademicProgramDetailPage));
            
            InitializeComponent();
        }
    }
}
