using System;
namespace XamarinNativeFullDemo.Data
{
    public static class Constants
    {
        public const string IS_ACCOUNT_SAVED = "IsAccountSaved";
		public const string PREFIX_ACCOUNT = "Account";
        public const string IS_EDIT = "isEdit";

        //Tarif plans
        public const int FREE_PLAN = 1;
        public const int FULL_PLAN = 10;

        //iOS Segues
        public const string SEGUE_FROM_CALC_TO_ACCOUNT = "CalcToAccount";
		public const string SEGUE_FROM_CALC_TO_BRAND = "CalcToBrand";
        public const string SEGUE_FROM_ACCOUNT_TO_CALC = "AccountToCalc";
        public const string SEGUE_FROM_ACCOUNT_TO_BRAND = "AccountToBrand";
        public const string SEGUE_FROM_BRAND_TO_ACCOUNT = "BrandToAccount";
		public const string SEGUE_FROM_CLOTHES_TO_BRAND = "ClothesToBrand";
		public const string SEGUE_FROM_BRAND_TO_CLOTHES = "BrandToClothes";


		//Localization Keys
        public const string BUTTON_SAVE = "BUTTON_SAVE";
        public const string BUTTON_IMPORT = "BUTTON_IMPORT";
        public const string BUTTON_EXPORT = "BUTTON_EXPORT";
        public const string BUTTON_CANCEL = "BUTTON_CANCEL";
		public const string BUTTON_OK = "BUTTON_OK";
        public const string BUTTON_NEW = "BUTTON_NEW";
		public const string BUTTON_DELETE = "BUTTON_DELETE";

		public const string TITLE_DELETE = "TITLE_DELETE";
		public const string TITLE_INFO = "TITLE_INFO";
        public const string TITLE_ERROR = "TITLE_ERROR";
        public const string TITLE_ADD_ACCOUNT = "TITLE_ADD_ACCOUNT";

		public const string MESSAGE_INFO = "MESSAGE_INFO";
        public const string MESSAGE_DELETE = "MESSAGE_DELETE";
        public const string MESSAGE_REQUIRED = "MESSAGE_REQUIRED";
        public const string MESSAGE_MIN_LENGTH = "MESSAGE_MIN_LENGTH";
        public const string MESSAGE_MAX_LENGTH = "MESSAGE_MAX_LENGTH";
        public const string MESSAGE_MIN_VALUE = "MESSAGE_MIN_VALUE";
        public const string MESSAGE_MAX_VALUE = "MESSAGE_MAX_VALUE";
		public const string MESSAGE_NUMERIC_PATTERN = "MESSAGE_NUMERIC_PATTERN";
        public const string MESSAGE_ALPHABETIC_PATTERN = "MESSAGE_ALPHABETIC_PATTERN";
        public const string MESSAGE_ALPHABETIC_LENGTH = "MESSAGE_ALPHABETIC_LENGTH";
        public const string MESSAGE_DUBLICATED_ACCOUNT = "MESSAGE_DUBLICATED_ACCOUNT";
		public const string MESSAGE_UNEXISTING_SIZE = "MESSAGE_UNEXISTING_SIZE";
        public const string MESSAGE_CHECK_CONNECTION = "MESSAGE_CHECK_CONNECTION";
		public const string MESSAGE_LOAD_ERROR = "MESSAGE_LOAD_ERROR";
        public const string MESSAGE_SCAN_CODE = "MESSAGE_SCAN_CODE";
        public const string MESSAGE_IMPORT_ERROR = "MESSAGE_IMPORT_ERROR";
		public const string MESSAGE_LOAD_DATA = "MESSAGE_LOAD_DATA";
		public const string MESSAGE_SELECT_ACCOUNT = "MESSAGE_SELECT_ACCOUNT";
        public const string MESSAGE_EDIT_WARNING = "MESSAGE_EDIT_WARNING";
		public const string MESSAGE_ACCOUNT_LIMIT = "MESSAGE_ACCOUNT_LIMIT";

        //Android actions
        public const string ADD_ACTION = "Add";
		public const string NEW_ACTION = "New";
		public const string IMPORT_ACTION = "Import";
        public const string EDIT_ACTION = "Edit";
		public const string SAVE_ACTION = "Save";

        //Import entries
        public const string ENTRY_ACCOUNT = "account";
        public const string ENTRY_ACCOUNT_DATA = "account_data";
    }
}
