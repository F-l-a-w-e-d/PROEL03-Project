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

        //Transfer to registration form
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Home());
            await Navigation.PushAsync(new Registration());
        }
    }
}
