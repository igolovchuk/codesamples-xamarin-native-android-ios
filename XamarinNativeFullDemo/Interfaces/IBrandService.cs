using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;

namespace XamarinNativeFullDemo.Interfaces
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand Get(int id);
    }
}
