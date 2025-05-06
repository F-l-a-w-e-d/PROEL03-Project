using Luna.Models;

namespace Luna
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // bind to ViewModel
            BindingContext = new LoginViewModel();
        }
    }
}
