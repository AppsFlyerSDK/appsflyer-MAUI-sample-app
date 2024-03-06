#if ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Android.Content;
using Com.Appsflyer;
using Java.Interop;
using Java.Util;


public class AppsFlyerListener : Java.Lang.Object, IAppsFlyerConversionListener
{
    public void OnAppOpenAttribution(IDictionary<string, string> p0)
    {
        throw new NotImplementedException();
    }

    public void OnAttributionFailure(string p0)
    {
        throw new NotImplementedException();
    }

    public void OnConversionDataFail(string p0)
    {
        throw new NotImplementedException();
    }

    public void OnConversionDataSuccess(IDictionary<string, Java.Lang.Object> p0)
    {
        Console.WriteLine("--- --- --- ---");
        foreach (KeyValuePair<string, Java.Lang.Object> kvp in p0)
        {
            Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
    }
}

public class AppsFlyerLibAndroid : IAppsFlyerLib
{
    private Context _context;

    public AppsFlyerLibAndroid(string devKey)
    {
        _context = Android.App.Application.Context;
        AppsFlyerLib.Instance.Init(devKey, null, _context);
    }

    public string GetSDKVersion()
    {
        return AppsFlyerLib.Instance.SdkVersion;
    }

    public void LogEvent(string eventName, Dictionary<string, string>? eventValues)
    {
        AppsFlyerLib.Instance.LogEvent(_context, eventName, eventValues as IDictionary<string, Java.Lang.Object>);
    }

    public void SetIsDebug(bool isDebug)
    {
        AppsFlyerLib.Instance.SetDebugLog(true);
    }

    public void Start()
    {
        AppsFlyerLib.Instance.RegisterConversionListener(_context, new AppsFlyerListener());
        AppsFlyerLib.Instance.Start(_context);
    }

    public void HandleURL(string url)
    {
        throw new NotImplementedException();
    }

}



#endif