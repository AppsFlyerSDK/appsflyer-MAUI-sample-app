using Microsoft.Extensions.Logging;
using AppsFlyer.NET.Lib;

namespace Demo.NET8.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		var af = AppsFlyerLibImpl.GetInstance()
            .InitializeiOS("H9xZweqPFhzBEtiDh2vDj", "1427570452")
            .InitializeAndroid("H9xZweqPFhzBEtiDh2vDj")
            .setIsDebug(true);

        af.Start();
        af.LogEvent("AF_Purchase", new Dictionary<string, string> {
            { "test", "val" },
            { "test2", "val2" }
        });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
