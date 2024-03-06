namespace MyMAUITest;

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
		var afInstance = AppsFlyerLibImpl
			.GetInstance()
			.InitializeiOS("afDevKey", "afAppId")
			.InitializeAndroid("afDevKey")
			.setIsDebug(true);
		afInstance.Start();
		return builder.Build();
	}
}

