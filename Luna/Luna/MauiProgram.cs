using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;

namespace Luna;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// using communityToolKit and MediaElement

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiCommunityToolkitMediaElement()
			.UseMauiCommunityToolkit()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif
		// Add views
		builder.Services.AddSingleton<Home>();
		builder.Services.AddSingleton<Registration>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
