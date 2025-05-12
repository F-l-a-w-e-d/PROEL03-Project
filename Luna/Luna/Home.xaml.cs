using Luna.Models;
using System.Threading.Tasks;

namespace Luna;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();

		BindingContext = new HomeViewModel();
	}

	// Go to creating of post -- CHANGE
    private async void NewPost_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new NewPost());
	}
}