using Luna.Models;

namespace Luna;

public partial class Registration : ContentPage
{   
	public Registration()
	{
		InitializeComponent();

        // bind to LoginViewModel
        BindingContext = new LoginViewModel();
	}

    //Go back to Log in form
    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}