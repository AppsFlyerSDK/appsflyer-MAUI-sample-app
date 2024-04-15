using System.Collections.Generic;
#if IOS
using AppsFlyerXamarinBinding;
#endif


namespace AppsFlyer.NET.Lib
{

    public sealed class AppsFlyerLibImpl
    {
        IAppsFlyerLib _appsFlyerLib;

#pragma warning disable CS8618 
        private AppsFlyerLibImpl() { }

        private static AppsFlyerLibImpl _instance;
#pragma warning restore CS8618

        private static readonly object _lock = new object();

        public static AppsFlyerLibImpl GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppsFlyerLibImpl();
                    }
                }
            }
            return _instance;
        }

        public AppsFlyerLibImpl InitializeiOS(string devKey, string appId)
        {
#if IOS
            _appsFlyerLib = new AppsFlyerLibiOS(devKey, appId);
#endif
            return this;
        }

        public AppsFlyerLibImpl InitializeAndroid(string devKey)
        {
#if ANDROID
            _appsFlyerLib = new AppsFlyerLibAndroid(devKey);
#endif
            return this;
        }

        public AppsFlyerLibImpl setIsDebug(bool isDebug)
        {
            _appsFlyerLib.SetIsDebug(isDebug);
            return this;
        }

        public void Start()
        {
            _appsFlyerLib.Start();
        }

        public string SDKVersion()
        {
            var ver = _appsFlyerLib.GetSDKVersion();
            return ver;
        }

        public void LogEvent(string eventName, Dictionary<string, string> eventValues)
        {
            _appsFlyerLib.LogEvent(eventName, eventValues);
        }

        public void HandleURL(string url)
        {
            _appsFlyerLib.HandleURL(url);
        }

        public AppsFlyerLibImpl setDelegate(AppsFlyerLibDelegate libDelegate)
        {
            _appsFlyerLib.setDelegate(libDelegate);
            return this;
        }

        public AppsFlyerLibImpl setDeepLinkDelegate(AppsFlyerDeepLinkDelegate deeplinkDelegate)
        {
            _appsFlyerLib.setDeepLinkDelegate(deeplinkDelegate);
            return this;
        }
    }

}