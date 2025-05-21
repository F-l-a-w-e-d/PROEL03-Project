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
}