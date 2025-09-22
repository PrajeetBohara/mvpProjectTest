namespace Dashboard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // A global route alias for home navigation
            Routing.RegisterRoute("//Home", typeof(Pages.HomePage));
        }
    }
}
