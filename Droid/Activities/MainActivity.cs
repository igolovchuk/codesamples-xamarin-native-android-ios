using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using XamarinNativeFullDemo.Droid.Adapters;
using XamarinNativeFullDemo.Interfaces;
using System.Collections.Generic;
using XamarinNativeFullDemo.Services;
using XamarinNativeFullDemo.Models;
using XamarinNativeFullDemo.Droid.Helpers;
using System.Linq;
using XamarinNativeFullDemo.Data;
using XamarinNativeFullDemo.Extensions;
using Android.Content;
using XamarinNativeFullDemo.Droid.Activities;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FortySevenDeg.SwipeListView;
using Java.Interop;
using Android.Net;
using ZXing;
using ZXing.Mobile;
using System.Threading.Tasks;
using Android.Gms.Ads;

namespace XamarinNativeFullDemo.Droid
{
    [Activity(Label = "Company Pro", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AdvertActivity
    {
        // private View _view;
        private SwipeListView _listView;
        private List<CT1Item> _accounts;
        private List<CT1Item> _filteredAccounts;
        private IAccountService _accountService;

        private static IFileSystemStrategy _filesystem;

        static MainActivity()
        {
            if (_filesystem == null)
                _filesystem = new DroidFileSystem();

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
			SetContentView(Resource.Layout.Accounts);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
          
            MobileBarcodeScanner.Initialize(Application);

            if (_accountService == null)
                _accountService = new AccountService(_filesystem);

            _accounts = _accountService.GetAll().ToCT1ItemList();
            _filteredAccounts = new List<CT1Item>();

            _listView = FindViewById<SwipeListView>(Resource.Id.accountList);
            _listView.Adapter = new CT1ActionAdapter(this, _accounts);

            InitListeners();
            //  _view = FindViewById(Resource.Id.main_activity);
            // _view.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.HideNavigation;
            //button.Click += delegate { button.Text = $"some clicks!"; };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void Close(object sender, System.EventArgs e)
        {
            //  Process.KillProcess(Process.MyPid());
            FinishAndRemoveTask();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ToString().Equals(Constants.ADD_ACTION))
            {
                if (_accounts.Count < AppSettings.AllowedAccounts)
                {
                    this.ShowTwoButtonAlert(AndroidLocalizator.Translate(Resource.String.add_account), Resource.String.import_account, Resource.String.new_account, () => Import(), () => New());
                }
                else
                {
                    this.ShowInfo(AndroidLocalizator.Translate(Resource.String.MESSAGE_ACCOUNT_LIMIT));
                }

            }
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            // ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var listView = sender as ListView;
            var row = _accounts[e.Position];
            var selected = _accountService.Get(row.Name.ToUserName());
            AppSettings.SelectedAccount = selected.UserName;
            AppSettings.IsFemale = selected.IsFemale;

            // var intent = new Intent(this, typeof(BrandActivity));

            // using(var stream = new MemoryStream()){
            //     var formatter = new BinaryFormatter();
            //     formatter.Serialize(stream, _accountService);
            //    intent.PutExtra(Constants.ACCOUNT_SERVICE, stream.ToArray());
            // }

            // intent.PutExtra(Constants.ACCOUNT_SERVICE, str);
            //  intent.PutExtra(Constants.FILESYSTEM, Convert.ToSByte(_filesystem));

            StartActivity(typeof(BrandActivity));
            //  Android.Widget.Toast.MakeText(this, t.Name, Android.Widget.ToastLength.Short).Show();
        }

        private void InitListeners()
        {
            var metrics = Resources.DisplayMetrics;
            float coef = (float)Resources.GetString(Resource.String.delete_percent).ToInt32() / 100;
            var offset = (float)(metrics.WidthPixels - coef * metrics.WidthPixels);
            _listView.SetOffsetLeft(offset);

            _listView.FrontViewClicked += HandleFrontViewClicked;
            _listView.BackViewClicked += HandleBackViewClicked;
            _listView.ListDataSetChanged += (s, e) => 
            {
                ((SwipeListView)s).CloseOpenedItems();
            };

            var sb = FindViewById<SearchView>(Resource.Id.searchView);
            sb.QueryTextChange += (s, e) =>
            {
                string text = e.NewText.ToLower();
                _filteredAccounts = _accounts.Where(ac => ac.Name.ToLower().Contains(text)).ToList();
                (_listView.Adapter as CT1Adapter).Update(_filteredAccounts);
            };
            sb.SetQueryHint(AndroidLocalizator.Translate(Resource.String.search_text));

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationIcon(Resource.Mipmap.clear);
            SetActionBar(toolbar);

            toolbar.NavigationOnClick += (s, e) => Close(s, e);

            var title = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            title.SetText(Resource.String.account_title);
        }

        private void HandleFrontViewClicked(object sender, SwipeListViewClickedEventArgs e)
        {
            var row = _accounts[e.Position];
            var selected = _accountService.Get(row.Name.ToUserName());
            AppSettings.SelectedAccount = selected.UserName;
            AppSettings.IsFemale = selected.IsFemale;
            StartActivity(typeof(BrandActivity));
        }

        private void HandleBackViewClicked(object sender, SwipeListViewClickedEventArgs e)
        {
            this.ShowOkCancelAlert(AndroidLocalizator.Translate(Constants.TITLE_DELETE), AndroidLocalizator.Translate(Constants.MESSAGE_DELETE), () => DeleteAccount(e.Position));
        }

        private void New()
        {
            StartActivity(typeof(CalculationActivity));
        }

        private async void Import()
        {
            var connectionInfo = ((ConnectivityManager)GetSystemService(ConnectivityService)).ActiveNetworkInfo;
            if (connectionInfo != null && connectionInfo.IsConnected)
            {
                var scanner = new MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result != null)
                {
                    var dialog = this.ShowProgress();
                    await Task.Run(() => _accountService.TryImport(result.Text)).ContinueWith(r =>
                 {
                     dialog.Dismiss();
                     if (r.Result)
                     {
                         _accounts = _accountService.GetAll().ToCT1ItemList();
                         (_listView.Adapter as CT1ActionAdapter).Update(_accounts);
                     }
                     else
                     {
                         this.ShowError(AndroidLocalizator.Translate(Resource.String.MESSAGE_IMPORT_ERROR));
                     }
                 }, TaskScheduler.FromCurrentSynchronizationContext());

                }
            }
            else
            {
                this.ShowError(AndroidLocalizator.Translate(Resource.String.MESSAGE_CHECK_CONNECTION));
            }
        }

        public void Export(string userName)
        {
            var dialog = this.ShowProgress();
            var connectionInfo = ((ConnectivityManager)GetSystemService(ConnectivityService)).ActiveNetworkInfo;
            if (connectionInfo != null && connectionInfo.IsConnected)
            {
                var deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                Task.Run(() => _accountService.TryExport(deviceId, userName)).ContinueWith(r =>
                {
                    dialog.Dismiss();
                    _listView.CloseOpenedItems();
                    if (r.Result)
                    {
                        var destUrl = Path.Combine(AppSettings.ApiEndpoint, $"{deviceId}_{userName}");
                        this.ShowImageAlert(userName.ToFullName(), GetQRCode(destUrl), AndroidLocalizator.Translate(Resource.String.MESSAGE_SCAN_CODE));
                    }
                    else
                    {
                        this.ShowError(AndroidLocalizator.Translate(Resource.String.MESSAGE_LOAD_ERROR));
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                this.ShowError(AndroidLocalizator.Translate(Resource.String.MESSAGE_CHECK_CONNECTION));
            }

        }

        private Android.Graphics.Bitmap GetQRCode(string url)
        {
            var writer = new ZXing.Mobile.BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 250,
                    Width = 250
                }
            };
            return writer.Write(url);
        }

        private void DeleteAccount(int position)
        {
            // RunOnUiThread (() => _listView.CloseAnimate (position));
            var row = _accounts[position];
            _accountService.Delete(row.Name.ToUserName());
            _accounts.Remove(row);
            (_listView.Adapter as CT1ActionAdapter).Update(_accounts);
        }

    }
}

