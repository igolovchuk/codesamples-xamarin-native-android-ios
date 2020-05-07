using System;
namespace XamarinNativeFullDemo.Models
{
    public class Brand
    {
        public int Id
        {
            get;
            set;
        }
   
        public string Name
        {
            get;
            set;
        }

        public string Icon
        {
            get;
            set;
        }

        public Brand(int id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}
