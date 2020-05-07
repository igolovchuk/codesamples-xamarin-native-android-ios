using System;
namespace XamarinNativeFullDemo.Interfaces
{
    public interface IFileSystemStrategy
    {
        string ReadFile(string path);
        void DeleteFile(string path);
        string[] GetFiles(string path);
        string Combine(string path);
        void SaveFile(string path, string content);
    }
}
