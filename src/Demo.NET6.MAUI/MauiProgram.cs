namespace Demo.NET6.MAUI;
using AppsFlyer.NET.Lib;
using AppsFlyerXamarinBinding;
using Foundation;
using UIKit;

public static class MauiProgram
{
    static GCDDelegate gcdDelegate = new GCDDelegate();
    static DeepLinkDelegate deeplinkDelegate = new DeepLinkDelegate();

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
            .setIsDebug(true)
            .setDelegate(gcdDelegate)
            .setDeepLinkDelegate(deeplinkDelegate);

        af.Start();
        af.LogEvent("AF_Purchase", new Dictionary<string, string> {
            { "test", "val" },
            { "test2", "val2" }
        });

        Console.WriteLine("--- UUID: ---");
        var UUID = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            Console.WriteLine(UUID);
        // builder.Services.AddSingleton<MainViewModel>();

        return builder.Build();
    }
}

public class GCDDelegate : AppsFlyerLibDelegate
{

    public override void OnConversionDataFail(NSError error)
    {
        throw new NotImplementedException();
    }


    [Export("onConversionDataSuccess:")]
    public override void OnConversionDataSuccess(NSDictionary conversionInfo)
    {
        Console.WriteLine("--- GCD: ---");
        Console.WriteLine(conversionInfo.ToString());
    }

}

public class DeepLinkDelegate : AppsFlyerDeepLinkDelegate
{

    [Export("didResolveDeepLink:")]
    public override void DidResolveDeepLink(AppsFlyerDeepLinkResult result)
    {
        Console.WriteLine("--- DidResolveDeepLink: ---");
        Console.WriteLine(result.deepLink.toString());
    }
}

