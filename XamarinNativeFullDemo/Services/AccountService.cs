using System;
using System.Linq;
using System.Collections.Generic;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Models;
using System.IO;
using System.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Specialized;
using XamarinNativeFullDemo.Data;
using System.Text.RegularExpressions;
using System.IO.Compression;
using XamarinNativeFullDemo.Extensions;

namespace XamarinNativeFullDemo.Services
{
    [Serializable]
    public class AccountService : IAccountService
    {
        private List<Account> _accountList;
        private IFileSystemStrategy _filesystem;
        private static AccountService _instance;
        private readonly string AccountFolder =  @"DataBase/Accounts";
        public static readonly string AccountDataFolder = @"DataBase/AccountData";

        public AccountService(IFileSystemStrategy filesystem)
        {
            _filesystem = filesystem;
            if (_accountList == null)
                _accountList = new List<Account>();
            
            LoadData();
        }

		public static AccountService Instance(IFileSystemStrategy filesystem)
		{
			if (_instance == null)
				_instance = new AccountService(filesystem);
			return _instance;
		}

        public List<Account> GetAll()
        {
            return _accountList;
        }

        public Account Get(string username){
            return _accountList.Find(ac => ac.UserName == username);
        }

        public void AddOrEdit(Account account){
            var existingItem = _accountList.Find(ac => ac.UserName == account.UserName);
            if(existingItem != null){
                _accountList.Remove(existingItem);
            }
            _accountList.Add(account);
            SaveToFile(account);
        }


        public void Delete(string username){
            var existingItem = _accountList.Find(ac => ac.UserName == username);
            if (existingItem != null)
            {
                _accountList.Remove(existingItem);
            }
            _filesystem.DeleteFile($"{AccountFolder}/{username}.json");
            _filesystem.DeleteFile($"{AccountDataFolder}/{username}_data.json");
        }

        public bool IsValid(Account account){
            var existingItem = _accountList.Find(ac => ac.UserName == account.UserName);
            return existingItem == null;
        }

        public bool TryImport(string url){
			using (var client = new HttpClient ()) {               
                var userName = url.Substring(url.IndexOf('_') + 1);
                var response = client.GetAsync (url).Result;

				if (response.IsSuccessStatusCode) {
                    try {
                        var content = response.Content.ReadAsStreamAsync ().Result;
                        using(var zip = new ZipArchive(content, ZipArchiveMode.Read)){
                            zip.GetEntry(Constants.ENTRY_ACCOUNT)?.ExtractToFile(_filesystem.Combine($"{AccountFolder}/{userName}.json"), true);
                            zip.GetEntry(Constants.ENTRY_ACCOUNT_DATA)?.ExtractToFile(_filesystem.Combine($"{AccountDataFolder}/{userName}_data.json"), true);
                        }
                        LoadData();
						return true;
                    } catch (Exception) {
                        return false;
                    }
                  
				}
                return false;
			}
        }

        public bool TryExport(string deviceId, string userName)
        {
            try {
                System.Net.ServicePointManager.DnsRefreshTimeout = 0;
                var client = new HttpForm(AppSettings.ApiEndpoint);
                client.AttachFile(userName, _filesystem.Combine($"{AccountFolder}/{userName}.json"));
                client.AttachFile($"{userName}_data", _filesystem.Combine($"{AccountDataFolder}/{userName}_data.json"));
                client.SetValue("deviceId", deviceId);

                var response = client.Submit();
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            } catch (Exception) {
                return false;
            }
           
        }

        private void LoadData(){
            _accountList.Clear();
           var files = _filesystem.GetFiles(AccountFolder);
      
          foreach (var file in files)
            {
              var data = JsonValue.Parse(_filesystem.ReadFile(file));
              var account = new Account(data["Name"], data["IsFemale"], data["A"], data["B"], data["C"], data["D"], data["E"], data["F"], data["G"], data["H"]);
             _accountList.Add(account);
            } 
        }

        private void SaveToFile(Account account){
            var builder = new StringBuilder();
            builder.AppendLine("{");           
            builder.AppendLine($"\"Name\" : \"{account.Name}\", ");
            builder.AppendLine($"\"IsFemale\" : \"{account.IsFemale}\", ");
            builder.AppendLine($"\"A\" : \"{account.A}\", ");
            builder.AppendLine($"\"B\" : \"{account.B}\", ");
            builder.AppendLine($"\"C\" : \"{account.C}\", ");
            builder.AppendLine($"\"D\" : \"{account.D}\", ");
            builder.AppendLine($"\"E\" : \"{account.E}\", ");
            builder.AppendLine($"\"F\" : \"{account.F}\", ");
            builder.AppendLine($"\"G\" : \"{account.G}\", ");
            builder.AppendLine($"\"H\" : \"{account.H}\" ");
            builder.AppendLine ("}");

            _filesystem.SaveFile($"{AccountFolder}/{account.UserName}.json", builder.ToString());
        }
    }
}
