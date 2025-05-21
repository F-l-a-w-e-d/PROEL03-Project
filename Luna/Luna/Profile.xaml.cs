using Luna.Models;

namespace Luna;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();

		// add context
		BindingContext = new ProfileViewModel();
	}
}