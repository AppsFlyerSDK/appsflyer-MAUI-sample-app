using System;
using System.Collections.Generic;

public interface IAppsFlyerLib
{
	void Start();
	void LogEvent(string eventName, Dictionary<string, string>? eventValues);
	string GetSDKVersion();
	void SetIsDebug(bool isDebug);
	void HandleURL(string url);
}

