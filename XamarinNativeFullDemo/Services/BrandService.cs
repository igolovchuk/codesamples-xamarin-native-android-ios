using System.Linq;
using XamarinNativeFullDemo.Extensions;
using System.Collections.Generic;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Models;
using System.IO;
using System.Json;
using XamarinNativeFullDemo.Data;
using System.Text;

namespace XamarinNativeFullDemo.Services
{
    public class BrandService : IBrandService
    {
		private readonly string BrandFolder =  @"DataBase/Brands/BrandList.json";
        private readonly string AccountDataPath = $"{AccountService.AccountDataFolder}/{AppSettings.SelectedAccount}_data.json";

		private List<Brand> _brandList;
		private IFileSystemStrategy _filesystem;
        private JsonValue _sizeData;

        public BrandService(IFileSystemStrategy filesystem)
		{
            _filesystem = filesystem;
            
			if (_brandList == null)
                _brandList = new List<Brand>();
            
            if(_sizeData == null)
                _sizeData = JsonValue.Parse(_filesystem.ReadFile(AccountDataPath));

			LoadData();
		}

        public List<Brand> GetAll()
		{
            return _brandList.OrderBy(b => b.Name).ToList();
		}

		public Brand Get(int id)
		{
            return _brandList.Find(x => x.Id == id);
		}

		private void LoadData()
		{
            var data = JsonValue.Parse(_filesystem.ReadFile(BrandFolder));
            var syncList = new List<string>();

            foreach (KeyValuePair<string, JsonValue> item in data)
            {
                var brand = new Brand(item.Key.ToInt32(), item.Value["Name"], item.Value["Icon"]);
                _brandList.Add(brand);

                //if new brand was added but account hasn't calculation for it
                if(!_sizeData.ContainsKey(item.Key)){
                    syncList.Add(item.Key);
                }
            }

            if(syncList.Any()){
                SyncNewBrands(syncList);
            }
        }

        private void SyncNewBrands(List<string> syncList){            
			var builder = new StringBuilder();
			var accountData = AccountService.Instance(_filesystem).Get(AppSettings.SelectedAccount);
			var suffix = accountData.IsFemale ? "female" : "male";
			var dataFolder = $"DataBase/Brands/BrandData_{suffix}.json";

            var brandData = JsonValue.Parse(_filesystem.ReadFile(dataFolder));
            CalculationService.Instance(_filesystem).SetGeneralItem(new KeyValuePair<string, JsonValue>("0", brandData["0"]));

            foreach (var key in syncList)
            {
                var itemData = new KeyValuePair<string, JsonValue>(key, brandData[key]);
				CalculationService.Instance(_filesystem).BuldElement(builder, itemData, accountData);
            }

			builder.AppendLine("}");

			var file = _filesystem.ReadFile(AccountDataPath);
			file = file.Remove(file.LastIndexOf('}'), 1) + builder;
			_filesystem.SaveFile(AccountDataPath, file);
        }
    }
}
