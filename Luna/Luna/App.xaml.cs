namespace Luna
{
    public partial class App : Application
    {
        // Service to provide general navigation access
        public static IServiceProvider Services { get; private set; }

        [Obsolete]
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;
            // changed to navigatio page and to splashScreen
            MainPage = new NavigationPage(new splashScreen());
        }
    }
}