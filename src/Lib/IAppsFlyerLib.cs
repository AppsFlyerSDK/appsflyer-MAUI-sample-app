using System;
using System.Collections.Generic;
#if IOS
using AppsFlyerXamarinBinding;
#endif

namespace AppsFlyer.NET.Lib
{
	public interface IAppsFlyerLib
	{
		void Start();
		void LogEvent(string eventName, Dictionary<string, string>? eventValues);

		string GetSDKVersion();
		void SetIsDebug(bool isDebug);
		void HandleURL(string url);
		void setDelegate(AppsFlyerLibDelegate libDelegate);
		void setDeepLinkDelegate(AppsFlyerDeepLinkDelegate deeplinkDelegate);

	}
}
