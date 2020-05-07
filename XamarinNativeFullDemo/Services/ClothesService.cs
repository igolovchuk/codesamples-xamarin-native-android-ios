using System;
using System.Linq;
using System.Collections.Generic;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Models;
using System.IO;
using System.Json;
using XamarinNativeFullDemo.Data;
using System.Threading.Tasks;

namespace XamarinNativeFullDemo.Services
{
    public class ClothesService : IClothesService
    {
        private readonly string ClothesFolder = @"DataBase/Clothes/ClothesTypeList.json";
		private List<ClothesItem> _clothesList;
        private JsonValue _allSizeData;
		private IFileSystemStrategy _filesystem;

        public ClothesService(IFileSystemStrategy filesystem)
		{
            _filesystem = filesystem;
            if(_clothesList == null)
           _clothesList = new List<ClothesItem>();
            
            LoadData();
		}

        public List<ClothesItem> GetAll()
		{
            return _clothesList;
		}

        public void Update(IEnumerable<ClothesItem> source)
		{
            var needUpdate = false;
            foreach (var clothesItem in _clothesList)
            {
                foreach (var item in source)
                {
                    if(clothesItem.TypeId == item.TypeId && clothesItem.Size != item.Size){
                        clothesItem.Size = item.Size;
                        _allSizeData[AppSettings.SelectedBrandId.ToString()][item.TypeId] = item.Size;
                        needUpdate = true;
                    }
                }
            }

            if(needUpdate ){
                Task.Run(() =>_filesystem.SaveFile($"{AccountService.AccountDataFolder}/{AppSettings.SelectedAccount}_data.json", _allSizeData.ToString()));  
            }
        }



		private void LoadData()
		{
            var data = JsonValue.Parse(_filesystem.ReadFile(ClothesFolder));
            _allSizeData = JsonValue.Parse(_filesystem.ReadFile($"{AccountService.AccountDataFolder}/{AppSettings.SelectedAccount}_data.json"));
            var brandData = _allSizeData[AppSettings.SelectedBrandId.ToString()];
			foreach (KeyValuePair<string, JsonValue> item in data)
			{
                var clothesItem = new ClothesItem(item.Key, item.Value["Name"], item.Value["Icon"], brandData[item.Key]);
                _clothesList.Add(clothesItem);
			}
		}
    }
}
