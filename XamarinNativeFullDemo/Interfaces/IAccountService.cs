using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;

namespace XamarinNativeFullDemo.Interfaces
{
    public interface IAccountService
    {
        void AddOrEdit(Account account);
        bool TryImport (string url);
        bool TryExport (string deviceId, string userName);
        void Delete(string username);
        bool IsValid(Account account);
        Account Get(string username);
        List<Account> GetAll();
    }
}
