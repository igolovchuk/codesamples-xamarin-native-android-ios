using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XamarinNativeFullDemo.Extensions;
using XamarinNativeFullDemo.Models;

namespace XamarinNativeFullDemo.Data
{
    public struct Validator
    {       
		public static string ValidateIntegerInput(string input, string propName,int minValue, int maxValue)
		{
            if (string.IsNullOrEmpty(input)) return $"{propName}: { Localizator.Translate(Constants.MESSAGE_REQUIRED) } \n";

			if (Regex.IsMatch(input, @"^[1-9][0-9]"))
			{
				if (input.Length > 3)
                    return $"{propName}: { Localizator.Translate(Constants.MESSAGE_MAX_LENGTH) } \n";
                else if(input.ToInt32() < minValue)
                    return $"{propName}: { Localizator.Translate(Constants.MESSAGE_MIN_VALUE) }({minValue})! \n";
                else if (input.ToInt32() > maxValue)
                    return $"{propName}: { Localizator.Translate(Constants.MESSAGE_MAX_VALUE) }({maxValue})! \n";
			}
			else
			{
                return $"{propName}: { Localizator.Translate(Constants.MESSAGE_NUMERIC_PATTERN) } \n";
			}
			return string.Empty;
		}

        public static string ValidateAlpahebicInput(string input, string propName)
        {
            propName = Localizator.Translate (propName);
            if (string.IsNullOrEmpty(input)) return $"{propName}: { Localizator.Translate(Constants.MESSAGE_REQUIRED) } \n";

            if (Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я ]+$"))
			{
				if (input.Length <= 3)
                    return $"{propName}: { Localizator.Translate(Constants.MESSAGE_MIN_LENGTH) } \n";
				else if (input.Length > 30)
                    return $"{propName}: { Localizator.Translate(Constants.MESSAGE_ALPHABETIC_LENGTH) } \n";
			}
            else {
                return $"{propName}: { Localizator.Translate(Constants.MESSAGE_ALPHABETIC_PATTERN) } \n";
			}
            return string.Empty;
        }

        public static bool ValidateClothesTypes(IEnumerable<ClothesItem> data)
		{
			var checkList = Mapper.GetSizeCheckList();
			var shoesCheckList = Mapper.GetShoesSizeList();

			bool isValid = true;
			foreach (var item in data)
			{
				if (isValid)
				{
					switch (item.TypeId)
					{
						case "Shoes":
							isValid = AppSettings.IsFemale ? shoesCheckList.Contains($"{item.Size.ToInt32() + 1 }")
														   : shoesCheckList.Contains(item.Size);
							break;
						case "Pants":
                            isValid = Mapper.GetPantsSizeList(AppSettings.IsFemale).Contains(item.Size);
							break;
						default:
							isValid = checkList.Contains(item.Size);
							break;
					}
				}
				else
				{
					break;
				}
			}
			return isValid;
		}
    }
}
