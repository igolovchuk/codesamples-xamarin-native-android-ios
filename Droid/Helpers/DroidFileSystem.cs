using System;
using System.IO;
using Android.App;
using Android.Content.Res;
using XamarinNativeFullDemo.Data;
using XamarinNativeFullDemo.Interfaces;

namespace XamarinNativeFullDemo.Droid.Helpers
{
    public class DroidFileSystem : IFileSystemStrategy
    {
        private static string _folderPrefix = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string _dbFolder = "DataBase";

        static DroidFileSystem(){
            SyncAssets(new string[] {"Accounts", "AccountData", "Brands", "Clothes"});
        }

        public string ReadFile(string path){
            return File.ReadAllText(Path.Combine(_folderPrefix, path));
        }

        public string[] GetFiles(string path){
            return Directory.GetFiles($"{_folderPrefix}/{path}");
        }

		public void SaveFile(string path, string content)
		{
			File.WriteAllText($"{_folderPrefix}/{path}", content);
		}

        public void DeleteFile(string path){
            var filePath = Path.Combine($"{_folderPrefix}/{path}");
            if (File.Exists(filePath))
			{
                File.Delete(filePath);
			} 
        }

        private static void SyncAssets(string[] foldersToSync)
		{
			foreach (string assetFolder in foldersToSync)
			{
                var assetPath = $"{_dbFolder}/{assetFolder}";
                var fullPath = $"{_folderPrefix}/{assetPath}";

                string[] files = Application.Context.Assets.List(assetPath);
                foreach (var file in files)
                {
                    using (var source = Application.Context.Assets.Open($"{assetPath}/{file}"))
					{
                        if (!Directory.Exists(fullPath))
						{
							Directory.CreateDirectory(fullPath);
						}
                        if(!assetFolder.Contains(Constants.PREFIX_ACCOUNT) && !File.Exists ($"{fullPath}/{file}")){
							using (var sr = new StreamReader (source)) {
								var content = sr.ReadToEnd ();
								File.WriteAllText ($"{fullPath}/{file}", content);
							}
						}

					}
				 }
            }
               
		}

        public string Combine(string path)
        {
            return Path.Combine(_folderPrefix, path);
        }
    }
}
