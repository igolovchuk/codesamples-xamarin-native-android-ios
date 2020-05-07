using System;
namespace XamarinNativeFullDemo.Models
{
    public class ClothesItem
    {
		public string TypeId { get; set; }

		public string Name { get; set; }

		public string Icon { get; set; }

        public string Size { get; set; }

        public ClothesItem(string typeid, string name, string icon, string size)
        {
            TypeId = typeid;
            Name = name;
            Icon = icon;
            Size = size;
        }
    }
}
