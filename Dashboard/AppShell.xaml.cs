namespace Dashboard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Ensure a global route alias for home navigation
            Routing.RegisterRoute("//Home", typeof(Pages.HomePage));
        }
    }
}
