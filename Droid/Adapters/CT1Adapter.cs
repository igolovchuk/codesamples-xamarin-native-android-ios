using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.Res;
using Android.Views;
using Android.Widget;

namespace XamarinNativeFullDemo.Droid.Adapters
{
    public class CT1Adapter : BaseAdapter<CT1Item>
    {
		protected List<CT1Item> items;
		protected Activity context;

        public CT1Adapter(Activity context, List<CT1Item> items) : base()
        {
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override CT1Item this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CT1, null);
            view.FindViewById<TextView>(Resource.Id.txtName).Text = item.Name;

            int resID = (int)typeof(Resource.Drawable).GetField(item.ImageLogo.Replace(".png", "")).GetValue(null);
            view.FindViewById<ImageView>(Resource.Id.imgLogo).SetImageResource(resID);
			return view;
		} 

        public void Update(List<CT1Item> items){
            this.items = items;
            NotifyDataSetChanged();
        }

    }
}
