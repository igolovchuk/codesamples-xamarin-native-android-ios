using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using XamarinNativeFullDemo.Droid.Helpers;

namespace XamarinNativeFullDemo.Droid.Adapters
{
    public class CT1TextAdapter : CT1Adapter
    {
        private bool _isEditMode;

        public CT1TextAdapter(Activity context, List<CT1Item> items) : base(context, items)
        {
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CT1Text, null);
			view.FindViewById<TextView>(Resource.Id.txtName).Text = AndroidLocalizator.Translate ($"CLOTHES_{item.Name.ToUpper ().Replace ("-", "_")}");

            var txtSize = view.FindViewById<EditText>(Resource.Id.txtSize);
            txtSize.Text = item.Text;
            txtSize.Enabled = _isEditMode;
            var bgRes = _isEditMode ? Resource.Drawable.back_bordered : Android.Resource.Color.Transparent;
            txtSize.SetBackgroundResource(bgRes);
            txtSize.FocusChange += (s, e) => {
                items [position].Text = txtSize.Text;
            };

            view.FindViewById<TextView>(Resource.Id.txtTypeId).Text = item.Id;

			int resID = (int)typeof(Resource.Drawable).GetField(item.ImageLogo.Replace(".png", "")).GetValue(null);
			view.FindViewById<ImageView>(Resource.Id.imgLogo).SetImageResource(resID);
			return view;
		}

        public void Refresh(){
            _isEditMode = !_isEditMode;
            NotifyDataSetChanged ();
        }

        public List<View> GetAllChildred(View view){
            if(!(view is ViewGroup)){
                return new List<View> { view };
            }

            var result = new List<View> ();
            var viewGroup = (ViewGroup)view;
            for (int i = 0; i < viewGroup.ChildCount; i++) {
                var child = viewGroup.GetChildAt (i);

                var childSubviewList = new List<View> ();
                childSubviewList.Add (view);
                childSubviewList.AddRange(GetAllChildred (child));

                result.AddRange (childSubviewList);
            }
            return result;
        }

    }
}
