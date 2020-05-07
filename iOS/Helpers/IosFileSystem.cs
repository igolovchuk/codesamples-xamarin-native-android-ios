using System;
using System.IO;
using XamarinNativeFullDemo.Interfaces;

namespace XamarinNativeFullDemo.iOS.Helpers
{
    public class IosFileSystem : IFileSystemStrategy
    {
        private static string _folderPrefix = Directory.GetCurrentDirectory();

		public string ReadFile(string path)
		{
           return File.ReadAllText(path);
		}

		public string[] GetFiles(string path)
		{
			return Directory.GetFiles(Path.Combine(_folderPrefix, path));
		}

        public void SaveFile(string path, string content){
            File.WriteAllText(Path.Combine(_folderPrefix, path), content);
        }

		public void DeleteFile(string path)
		{
			var filePath = Path.Combine(_folderPrefix, path);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}

		public string Combine(string path)
		{
			return Path.Combine(_folderPrefix, path);
		}
    }
}
