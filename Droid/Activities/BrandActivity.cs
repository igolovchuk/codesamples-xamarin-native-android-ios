
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinNativeFullDemo.Data;
using XamarinNativeFullDemo.Droid.Adapters;
using XamarinNativeFullDemo.Droid.Helpers;
using XamarinNativeFullDemo.Extensions;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Services;

namespace XamarinNativeFullDemo.Droid.Activities
{
    [Activity(Label = "BrandActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class BrandActivity : AdvertActivity
    {
		private ListView _listView;
		private List<CT1Item> _brands;
		private List<CT1Item> _filteredBrands;
		private static IAccountService _accountService;
        private static IBrandService _brandService;
		private static IFileSystemStrategy _filesystem;

        static BrandActivity()
        {
			if (_filesystem == null)
				_filesystem = new DroidFileSystem();
            if (_accountService == null)
                _accountService = new AccountService(_filesystem);
            if(_brandService == null)
			_brandService = new BrandService(_filesystem);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Main);
            base.OnCreate(savedInstanceState);
			
			_brands = _brandService.GetAll().ToCT1ItemList();
			_filteredBrands = new List<CT1Item>();

			_listView = FindViewById<ListView>(Resource.Id.accountList);
			_listView.Adapter = new CT1Adapter(this, _brands);
			_listView.ItemClick += OnListItemClick;


			var sb = FindViewById<SearchView>(Resource.Id.searchView);
			sb.QueryTextChange += (s, e) =>
			{
				string text = e.NewText.ToLower();
                _filteredBrands = _brands.Where(ac => ac.Name.ToLower().Contains(text)).ToList();
                (_listView.Adapter as CT1Adapter).Update(_filteredBrands);
			};
			sb.SetQueryHint (Resources.GetText (Resource.String.search_text));

			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationIcon(Resource.Mipmap.back);
			SetActionBar(toolbar);

			toolbar.NavigationOnClick += (s, e) => GoBack(s, e);

			var title = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            title.Text = AppSettings.SelectedAccount.ToFullName();
		}

        private void GoBack(object s, EventArgs e)
        {
            base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.edit_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            if (item.ToString().Equals(AndroidLocalizator.Translate(Resource.String.edit)))
			{
                var intent = new Intent(this, typeof(CalculationActivity));
                intent.PutExtra(Constants.IS_EDIT, true);
                StartActivity(intent);
			}
			//Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
			// ToastLength.Short).Show();
			return base.OnOptionsItemSelected(item);
		}

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AppSettings.SelectedBrandId = GetBrand()[e.Position].Id.ToInt32 ();
            StartActivity (typeof (ClothesActivity));
        }

        private List<CT1Item> GetBrand ()
		{
            return FindViewById<SearchView>(Resource.Id.searchView).IsInEditMode ? _filteredBrands : _brands;
		}

	}


}
