using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Extensions;
namespace XamarinNativeFullDemo.Data
{
    public class Mapper
    {
        public static string ShoesGetUAByEU(string eusize){
            return (eusize.ToInt32() - 1).ToString();
        }
        public static string ShoesGetUSAByEU(string eusize)
		{
            return GetEuUsaShoesList()[eusize];
		}
		public static string ShoesGetUKByEU(string eusize)
		{
            return GetEuUkShoesList()[eusize];
		}
		public static string PantsGetEUByInt(string intsize, bool isFemale)
		{
            return GetIntEuPantsList(isFemale)[intsize];
		}

        private static Dictionary<string, string> GetEuUsaShoesList(){
            var dict = new Dictionary<string, string>();

			dict.Add("34", "3");
			dict.Add("35", "3.5");
            dict.Add("36", "4");
			dict.Add("37", "4.5");
			dict.Add("38", "5.5");
			dict.Add("39", "6");
			dict.Add("40", "7");
			dict.Add("41", "8");
			dict.Add("42", "9");
			dict.Add("43", "9.5");
			dict.Add("44", "10");
			dict.Add("45", "11");
			dict.Add("46", "12");
			dict.Add("47", "12.5");
			dict.Add("48", "13");
			dict.Add("49", "14");
			dict.Add("50", "15");
			dict.Add("51", "16");
			dict.Add("52", "17");
			dict.Add("53", "18");
			dict.Add("54", "19");
			dict.Add("55", "20");

            return dict;
        }

		private static Dictionary<string, string> GetEuUkShoesList()
		{
			var dict = new Dictionary<string, string>();

            dict.Add("34", "2.5");
            dict.Add("35", "3");
			dict.Add("36", "3.5");
			dict.Add("37", "4");
			dict.Add("38", "5");
			dict.Add("39", "6");
			dict.Add("40", "7");
			dict.Add("41", "7.5");
			dict.Add("42", "8");
			dict.Add("43", "9");
			dict.Add("44", "9.5");
			dict.Add("45", "10.5");
			dict.Add("46", "11");
			dict.Add("47", "12");
			dict.Add("48", "13");
			dict.Add("49", "13.5");
			dict.Add("50", "14.5");
			dict.Add("51", "15.5");
			dict.Add("52", "16.5");
			dict.Add("53", "17.5");
			dict.Add("54", "18.5");
			dict.Add("55", "19.5");

			return dict;
		}


		private static Dictionary<string, string> GetIntEuPantsList(bool isFemale)
		{
			var dict = new Dictionary<string, string>();

            if(!isFemale){
				dict.Add("XS", "30");
				dict.Add("S", "31");
				dict.Add("M", "32");
				dict.Add("L", "33");
				dict.Add("XL", "34");
				dict.Add("XXL", "36");
				dict.Add("3XL", "38");
				dict.Add("4XL", "40");
            }
            else {
				dict.Add("XS", "34");
				dict.Add("S", "36");
				dict.Add("M", "38");
				dict.Add("L", "40");
				dict.Add("XL", "42");
				dict.Add("XXL", "44");
            }			
			
			return dict;
		}


		public static List<string> GetSizeCheckList()
		{
			return new List<string>
			{
				"XS", "S","M","L","XL","XXL","3XL","4XL"
			};
		}

		public static List<string> GetShoesSizeList()
		{
			return new List<string>
			{
				"36", "37","38","39","40","41","42","43","44","45",
				"46","47","48","49","50","51","52","53","54","55"
			};
		}

		public static List<string> GetPantsSizeList(bool isFemale)
		{
            if(!isFemale){
					return new List<string> { "30","31","32","33","34","36","38","40","42","44","46","48","50" }; 
            } else {
					return new List<string> { "34", "36","38","40","42","44","46","48","50","52","54","56" }; 
            }
			
		}

        public static List<string> GetDigitalSizeList()
		{
			return new List<string>
			{
				"38-40", "40-42", "42-44", "44-46", "46-48", "48-50", "50-52", "52-54"
			};
		}
    }
}
