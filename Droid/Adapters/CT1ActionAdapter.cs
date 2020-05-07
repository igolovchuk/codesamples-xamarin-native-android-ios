using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Provider;
using XamarinNativeFullDemo.Extensions;
using Android.Net;

namespace XamarinNativeFullDemo.Droid.Adapters
{
    public class CT1ActionAdapter :  CT1Adapter
    {
		public CT1ActionAdapter (Activity context, List<CT1Item> items) : base(context, items)
        {
		}


		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = items [position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate (Resource.Layout.CT1Action, null);
			view.FindViewById<TextView> (Resource.Id.txtName).Text = item.Name;
            view.FindViewById <TextView>(Resource.Id.txtActionExport).Click += (s,e) => BtnExportClick (position);

			int resID = (int)typeof (Resource.Drawable).GetField (item.ImageLogo.Replace (".png", "")).GetValue (null);
			view.FindViewById<ImageView> (Resource.Id.imgLogo).SetImageResource (resID);
			return view;
		}

        private void BtnExportClick (int position)
        {
          var userName = items [position].Name.ToUserName ();
           var activity = (context as MainActivity);
            if (activity != null) {
                activity.Export (userName);
            }
        }
    }
}
