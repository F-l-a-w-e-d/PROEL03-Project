namespace Luna
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();
            // changed to navigatio page and to splashScreen
            MainPage = new NavigationPage(new splashScreen());
        }

    }
}