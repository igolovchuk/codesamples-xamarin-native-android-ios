
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
using Android.Views.InputMethods;
using Android.Widget;
using XamarinNativeFullDemo.Data;
using XamarinNativeFullDemo.Droid.Adapters;
using XamarinNativeFullDemo.Droid.Helpers;
using XamarinNativeFullDemo.Extensions;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Models;
using XamarinNativeFullDemo.Services;

namespace XamarinNativeFullDemo.Droid.Activities
{
    [Activity(Label = "ClothesActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ClothesActivity : AdvertActivity
    {
        private ListView _listView;
		private List<CT1Item> _clothesItems;
		private List<CT1Item> _filteredClothesItems;
		private IClothesService _clothesService;
		private static IBrandService _brandService;
        private static IFileSystemStrategy _filesystem;

		static ClothesActivity ()
		{
			if (_filesystem == null)
				_filesystem = new DroidFileSystem ();			
			if (_brandService == null)
                _brandService = new BrandService (_filesystem);
		}
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Main);
            base.OnCreate(savedInstanceState);

			_clothesService = new ClothesService (_filesystem);

            _clothesItems = _clothesService.GetAll().ToCT1ItemList();
			_filteredClothesItems = new List<CT1Item> ();

			_listView = FindViewById<ListView> (Resource.Id.accountList);
            _listView.Adapter = new CT1TextAdapter (this, _clothesItems);
            _listView.DescendantFocusability = DescendantFocusability.AfterDescendants;

			var sb = FindViewById<SearchView> (Resource.Id.searchView);
			sb.QueryTextChange += (s, e) => {
				string text = e.NewText.ToLower ();
				_filteredClothesItems = _clothesItems.Where (ac => ac.Name.ToLower ().Contains (text)).ToList ();
				(_listView.Adapter as CT1TextAdapter).Update (_filteredClothesItems);
			};

			sb.SetQueryHint (Resources.GetText (Resource.String.search_text));

			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			toolbar.SetNavigationIcon (Resource.Mipmap.back);
			SetActionBar (toolbar);

			toolbar.NavigationOnClick += (s, e) => GoBack (s, e);

			var title = (TextView)toolbar.FindViewById (Resource.Id.toolbar_title);
            title.Text = _brandService.Get(AppSettings.SelectedBrandId).Name;
			
            _listView.RequestFocus ();
        }

		private void GoBack (object s, EventArgs e)
		{
			base.OnBackPressed ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.edit_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
            if (item.ToString().Equals(AndroidLocalizator.Translate (Resource.String.edit))) {               
                item.SetTitle (Resource.String.save);
				item.SetIcon (Resource.Mipmap.save);
                (_listView.Adapter as CT1TextAdapter).Refresh();

                var firstItem = _listView.GetChildAt (_listView.FirstVisiblePosition).FindViewById<EditText> (Resource.Id.txtSize);
               
                firstItem.RequestFocus ();
				var imm = (InputMethodManager)GetSystemService (InputMethodService);
                imm.ShowSoftInput (firstItem, ShowFlags.Implicit);
			}
            else if(item.ToString().Equals(AndroidLocalizator.Translate (Resource.String.save))){   
                _listView.RequestFocus ();

                CT1Item ct1Item;
				var updateList = new List<ClothesItem> ();
        
                for (int i = 0; i < _listView.Count; i++) {
                    ct1Item = _listView.GetItemAtPosition(i).Cast<CT1Item>();
                    updateList.Add (new ClothesItem (ct1Item.Id, string.Empty, string.Empty, ct1Item.Text));
				}

				if (Validator.ValidateClothesTypes (updateList)) {
					_clothesService.Update (updateList);
					item.SetTitle (Resource.String.edit);
					item.SetIcon (Resource.Mipmap.edit);
         			(_listView.Adapter as CT1TextAdapter).Refresh ();
				} else {
					this.ShowError (AndroidLocalizator.Translate (Constants.MESSAGE_UNEXISTING_SIZE));
				}
               
            }
			//Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
			// ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}

	}
}
