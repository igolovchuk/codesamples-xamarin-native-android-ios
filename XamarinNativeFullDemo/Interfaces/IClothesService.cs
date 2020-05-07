using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;

namespace XamarinNativeFullDemo.Interfaces
{
    public interface IClothesService
    {
        List<ClothesItem> GetAll();
        void Update(IEnumerable<ClothesItem> source);
    }
}
