using AppsFlyerXamarinBinding;
using Foundation;
using UIKit;

namespace Demo.NET6.MAUI;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    [Export("application:continueUserActivity:restorationHandler:")]
    public override bool ContinueUserActivity(UIKit.UIApplication application, NSUserActivity userActivity, UIKit.UIApplicationRestorationHandler completionHandler)
    {
        AppsFlyerLib.Shared.ContinueUserActivity(userActivity, completionHandler);
        return base.ContinueUserActivity(application, userActivity, completionHandler);
    }


    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
        AppsFlyerLib.Shared.handleOpenUrl(url, options);
        return base.OpenUrl(application, url, options);
    }
}


