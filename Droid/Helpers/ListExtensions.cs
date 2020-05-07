using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;
using XamarinNativeFullDemo.Droid.Adapters;
using System.Linq;
using Android.OS;
using System.Json;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace XamarinNativeFullDemo.Droid.Helpers
{
    public static class ListExtensions
    {
        public static List<CT1Item> ToCT1ItemList(this List<Account> list){
            return list.Select(x => new CT1Item 
            { ImageLogo = x.Icon,
              Name = x.Name 
            })
            .ToList();
        }

        public static List<CT1Item> ToCT1ItemList(this List<Brand> list)
		{
			return list.Select(x => new CT1Item
			{
                Id = x.Id.ToString (),
				ImageLogo = x.Icon,
				Name = x.Name
			})
			.ToList();
		}

        public static List<CT1Item> ToCT1ItemList (this List<ClothesItem> list)
		{
			return list.Select (x => new CT1Item {
                Id = x.TypeId,
				ImageLogo = x.Icon,
				Name = x.Name,
                Text = x.Size
			})
			.ToList ();
		}

        public static T Cast<T>(this Java.Lang.Object obj) where T : class{
            var propInfo = obj.GetType ().GetProperty ("Instance");
            return propInfo != null ? propInfo.GetValue (obj, null) as T : null;
        }
    }
}
