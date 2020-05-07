using Android.App;

namespace XamarinNativeFullDemo.Droid.Helpers
{
    public struct AndroidLocalizator
    {
        public static string Translate(string key)
        {
            int resID = (int)typeof (Resource.String).GetField (key).GetValue (null);
            string result = Application.Context.Resources.GetText (resID);
            return result;
        }

		public static string Translate (int resID)
		{
			return Application.Context.Resources.GetText (resID);
		}
    }
}
