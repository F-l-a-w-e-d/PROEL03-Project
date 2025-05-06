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
}