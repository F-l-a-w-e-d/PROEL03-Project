using Luna.Models;

namespace Luna;

public partial class NewPost : ContentPage
{
	// Initialize
	public NewPost()
	{
		InitializeComponent();

		// add binding context
		BindingContext = new PostViewModel();
	}
}