using System;
namespace XamarinNativeFullDemo.Data
{
    public static class AppSettings
    {
		public static string SelectedAccount { get; set; }
		public static bool IsFemale { get; set; }
		public static int SelectedBrandId { get; set; }
        public static int AllowedAccounts => 10;
    }
}
