using System;
using Foundation;

namespace XamarinNativeFullDemo.iOS.Helpers
{
    public struct IosLocalizator 
    {
        public static string Translate(string key)
        {
            return NSBundle.MainBundle.LocalizedString(key, string.Empty);
        }
    }
}
