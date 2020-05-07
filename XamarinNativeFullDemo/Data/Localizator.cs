using System;
namespace XamarinNativeFullDemo.Data
{
    public class Localizator
    {
        public static string Translate(string key)
        {
#if __IOS__
           
            return iOS.Helpers.IosLocalizator.Translate(key);
#else
            return  Droid.Helpers.AndroidLocalizator.Translate(key);
#endif
		}
    }
}
