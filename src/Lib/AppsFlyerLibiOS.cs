#if IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using AppsFlyerXamarinBinding;
using Foundation;
using UIKit;
using static SystemConfiguration.NetworkReachability;

namespace AppsFlyer.NET.Lib
{
    public class AppsFlyerLibiOSDelegate : AppsFlyerLibDelegate
    {

        public override void OnConversionDataFail(NSError error)
        {
            throw new NotImplementedException();
        }


        public override void OnConversionDataSuccess(NSDictionary conversionInfo)
        {
            Console.WriteLine("--- --- --- S --- --- ---");
            Console.WriteLine(conversionInfo.ToString());
        }
    }


    public class AppsFlyerLibiOS: IAppsFlyerLib
    {
        NSObject DidBecomeActiveHandle;

        public AppsFlyerLibiOS(string devKey, string appId)
        {
            AppsFlyerLib.Shared.AppsFlyerDevKey = devKey;
            AppsFlyerLib.Shared.AppleAppID = appId;
            AppsFlyerLib.Shared.IsDebug = true;
            AppsFlyerLib.Shared.AppInviteOneLinkID = "TSVA";
            DidBecomeActiveHandle = UIApplication.Notifications.ObserveDidBecomeActive(DidBecomeActiveCallback);
        }

        public string GetSDKVersion()
        {
            return AppsFlyerLib.Shared.SDKVersion;
        }

        public void LogEvent(string eventName, Dictionary<string, string>? eventValues)
        {
            return;
            NSDictionary<NSString, NSObject>? _eventValues = null;

            if (eventValues != null)
            {
                var asObjectMap = eventValues.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
                _eventValues = ConvertToNativeDictionary(asObjectMap);
            }

            AppsFlyerLib.Shared.LogEventWithEventName(eventName: eventName, eventValues: _eventValues, null);
        }

        private static NSDictionary<NSString, NSObject> ConvertToNativeDictionary(Dictionary<string, object> dict)
        {
            NSMutableDictionary<NSString, NSObject> newDictionary = new NSMutableDictionary<NSString, NSObject>();

            try
            {
                foreach (string k in dict.Keys)
                {
                    var value = dict[k];

                    try
                    {
                        if (value is string)
                            newDictionary.Add((NSString)k, new NSString((string)value));
                        else if (value is int)
                            newDictionary.Add((NSString)k, new NSNumber((int)value));
                        else if (value is float)
                            newDictionary.Add((NSString)k, new NSNumber((float)value));
                        else if (value is nfloat)
                            newDictionary.Add((NSString)k, new NSNumber((nfloat)value));
                        else if (value is double)
                            newDictionary.Add((NSString)k, new NSNumber((double)value));
                        else if (value is bool)
                            newDictionary.Add((NSString)k, new NSNumber((bool)value));
                        else
                            newDictionary.Add((NSString)k, new NSString(value.ToString()));
                    }
                    catch (Exception Ex)
                    {
                        if (value != null)
                            Ex.Data.Add("value", value);

                        // Logger.LogException(Ex);
                        continue;
                    }
                }
            }
            catch (Exception Ex)
            {
                // Logger.LogException(Ex);
            }

          
            NSDictionary<NSString, NSObject> keyValuePairs = new NSDictionary<NSString, NSObject>(newDictionary.Keys, newDictionary.Values);

            return keyValuePairs;
        }

        public void SetIsDebug(bool isDebug)
        {
            AppsFlyerLib.Shared.IsDebug = isDebug;
        }

        public void Start()
        {
            AppsFlyerLib.Shared.Start();
        }

        public void HandleURL(string url)
        {


            /* https://github.com/dotnet/maui/issues/14671

             switch (userActivity.ActivityType)
    {
        case "NSUserActivityTypeBrowsingWeb":
            strLink = userActivity.WebPageUrl.AbsoluteString;
            break;
        case "com.apple.corespotlightitem":
            if (userActivity.UserInfo.ContainsKey(CSSearchableItem.ActivityIdentifier))
                strLink = userActivity.UserInfo.ObjectForKey(CSSearchableItem.ActivityIdentifier).ToString();
            break;
        default:
            if (userActivity.UserInfo.ContainsKey(new NSString("link")))
                strLink = userActivity.UserInfo[new NSString("link")].ToString();
            break;
    }
             
             */

            AppsFlyerLib.Shared.HandleOpenURL(new NSUrl(url), null);
        }

        void DidBecomeActiveCallback(object sender, NSNotificationEventArgs args)
        {
            Start();
        }

        // Clean It Up
        void Teardown()
        {
            DidBecomeActiveHandle.Dispose();
        }

        public void setDelegate(AppsFlyerLibDelegate libDelegate)
        {
            AppsFlyerLib.Shared.Delegate = libDelegate;
        }

        public void setDeepLinkDelegate(AppsFlyerDeepLinkDelegate deeplinkDelegate)
        {
            AppsFlyerLib.Shared.DeepLinkDelegate = deeplinkDelegate;
        }
    }
}

#endif