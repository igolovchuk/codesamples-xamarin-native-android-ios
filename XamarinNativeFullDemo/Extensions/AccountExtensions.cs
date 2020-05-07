using System;
using System.Globalization;

namespace XamarinNativeFullDemo.Extensions
{
    public static class AccountExtensions
    {
        public static string ToUserName(this string name){
            return name.Replace(" ", "_");
        }

		public static string ToFullName(this string username)
		{
            return username.Replace("_", " ");
		}

		public static int ToInt32(this string value)
		{
			int number;

			Int32.TryParse(value, out number);

			return number;
		}

        public static float ToFloat(this string value)
		{
            float number;

            float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out number);

			return number;
		}
    }
}
