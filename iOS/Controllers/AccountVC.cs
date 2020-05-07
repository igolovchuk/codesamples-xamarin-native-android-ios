using System;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.Data;
using XamarinNativeFullDemo.iOS.Cells;
using XamarinNativeFullDemo.iOS.Helpers;
using XamarinNativeFullDemo.Services;
using XamarinNativeFullDemo.Extensions;
using UIKit;
using Foundation;
using System.Linq;
using System.Threading;
using System.Collections.Specialized;
using System.Text;
using System.Net.Http;
using System.IO;
using ZXing.Mobile;
using ZXing;
using XamarinNativeFullDemo.iOS.Controls;
using System.Threading.Tasks;

namespace XamarinNativeFullDemo.iOS.Controllers
{
    public partial class AccountVC : UIViewController
    {
        private List<Account> _accounts;
        private List<Account> _filteredAccounts;
        private IAccountService _accountService;
        private static IFileSystemStrategy _filesystem;
        private static LoadingOverlay _loadPopup;

        static AccountVC(){
            if (_filesystem == null)
                _filesystem = new IosFileSystem();
            if(_loadPopup == null)
			_loadPopup = new LoadingOverlay (UIScreen.MainScreen.Bounds);
        }

        public AccountVC(IntPtr handle) : base(handle)
        {
            _accountService = new AccountService(_filesystem);
            _accounts = _accountService.GetAll();
            _filteredAccounts = new List<Account>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var accountSource = new AccountSource(this);
            tableView.Source = accountSource;

			var sdc = searchDisplayController;
            sdc.SearchResultsSource = accountSource;
            sdc.SearchResultsTableView.RowHeight = tableView.RowHeight;
			sdc.SearchBar.TextChanged += (sender, e) =>
				{
                 string text = e.SearchText.ToLower();
                _filteredAccounts = _accounts.Where(ac => ac.Name.ToLower().Contains(text)).ToList();
				};
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		partial void btnExit_Click(UIBarButtonItem sender)
		{
            Thread.CurrentThread.Abort();
		}

		partial void btnAdd_Click(UIBarButtonItem sender)
		{
            if (_accounts.Count < AppSettings.AllowedAccounts)
            {
                // Create a new Alert Controller
                UIAlertController actionSheetAlert = UIAlertController.Create(IosLocalizator.Translate(Constants.TITLE_ADD_ACCOUNT), IosLocalizator.Translate(Constants.MESSAGE_SELECT_ACCOUNT), UIAlertControllerStyle.ActionSheet);

                // Add Actions
                actionSheetAlert.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_NEW), UIAlertActionStyle.Default, (action) => PerformSegue(Constants.SEGUE_FROM_ACCOUNT_TO_CALC, this)));
                actionSheetAlert.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_IMPORT), UIAlertActionStyle.Default, (action) => Import()));
                actionSheetAlert.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_CANCEL), UIAlertActionStyle.Cancel, (action) => { }));

                // Display the alert
                PresentViewController(actionSheetAlert, true, null);
            }
			else
			{
                this.ShowInfo(IosLocalizator.Translate(Constants.MESSAGE_ACCOUNT_LIMIT));
			}
           
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			// do first a control on the Identifier for your segue
            if (segue.Identifier.Equals(Constants.SEGUE_FROM_ACCOUNT_TO_BRAND))
			{
                var viewController = (BrandVC)segue.DestinationViewController;
                viewController.SetAccountService(_accountService);
			}
            else if(segue.Identifier.Equals(Constants.SEGUE_FROM_ACCOUNT_TO_CALC)){
                var calcVC = (CalculationVC)segue.DestinationViewController;
                calcVC.SetAccountService(_accountService);
            }
		}

		private async void Import ()
		{
            if (Reachability.IsHostReachable(AppSettings.ApiEndpoint))
			{
			var scanner = new MobileBarcodeScanner (this);
			var result = await scanner.Scan ();
            if(result == null){
                View.Add (_loadPopup);
                    await Task.Run(() => _accountService.TryImport(result.Text)).ContinueWith(r =>
                   {
                       if (r.Result)
                       {
                           _loadPopup.Hide();
                           tableView.ReloadData();
                       }
                       else
                       {
                           this.ShowError(IosLocalizator.Translate(Constants.MESSAGE_IMPORT_ERROR));
                       }
                   }, TaskScheduler.FromCurrentSynchronizationContext());
              }
			}
			else
			{
				this.ShowError(IosLocalizator.Translate(Constants.MESSAGE_CHECK_CONNECTION));
			}

		}

		//data source
		private class AccountSource : UITableViewSource
		{
			private AccountVC accountController;
			private UISearchDisplayController search;

			public AccountSource(AccountVC brandController)
			{
				this.accountController = brandController;
				search = brandController.searchDisplayController;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
                tableview.RegisterNibForCellReuse(CT1.Nib, CT1.Key);
				return GetAccount().Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{				
                Account account = GetAccount()[indexPath.Row];
				var cell = ( CT1)tableView.DequeueReusableCell( CT1.Key);
                cell.SetCellData(account.Name, account.Icon);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
                var selectedAcoount = GetAccount()[indexPath.Row];
                AppSettings.SelectedAccount = selectedAcoount.UserName;
                AppSettings.IsFemale = selectedAcoount.IsFemale;

                accountController.PerformSegue(Constants.SEGUE_FROM_ACCOUNT_TO_BRAND, accountController);
			}
		
			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
				return true; // return false if you wish to disable editing for a specific indexPath or for all rows
			}
            
            public override UITableViewRowAction [] EditActionsForRow (UITableView tableView, NSIndexPath indexPath)
            {
                        var deleteButton = UITableViewRowAction.Create (UITableViewRowActionStyle.Default, IosLocalizator.Translate(Constants.BUTTON_DELETE), (s, e) => {
                    accountController.ShowOkCancelAlert (IosLocalizator.Translate (Constants.TITLE_DELETE), IosLocalizator.Translate (Constants.MESSAGE_DELETE), () => DeleteAccount (indexPath));
                });

                        var exportButton = UITableViewRowAction.Create (UITableViewRowActionStyle.Normal, IosLocalizator.Translate(Constants.BUTTON_EXPORT), (s,e) => Export (indexPath));
                return new UITableViewRowAction[] {deleteButton, exportButton };
            }

            private List<Account> GetAccount()
			{
                return search.Active ? accountController._filteredAccounts : accountController._accounts;
			}

            private void DeleteAccount(NSIndexPath indexPath)
			{
                var userName = GetAccount()[indexPath.Row].UserName;
                accountController._accountService.Delete(userName);
				// remove the item from the underlying data source
				//GetAccount().RemoveAt(indexPath.Row);
				// delete the row from the table
                accountController.tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Left);
			}

            private void Export(NSIndexPath indexPath)
			{			
                if (Reachability.IsHostReachable(AppSettings.ApiEndpoint))
                {
                    accountController.View.Add(_loadPopup);

                    var userName = GetAccount()[indexPath.Row].UserName;
                    var deviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();

                    Task.Run(() => accountController._accountService.TryExport(deviceId, userName)).ContinueWith(r =>
                 {
					 _loadPopup.Hide();
                  if (r.Result)
                  {
                      var destUrl = Path.Combine(AppSettings.ApiEndpoint, $"{deviceId}_{userName}");
                      accountController.ShowImageAlert(userName.ToFullName(), GetQRCode(destUrl), IosLocalizator.Translate(Constants.MESSAGE_SCAN_CODE));
                  }
                  else
                  {
                      accountController.ShowError(IosLocalizator.Translate(Constants.MESSAGE_LOAD_ERROR));
                  }
                 
                  }, TaskScheduler.FromCurrentSynchronizationContext());

              } else 
                {
                  accountController.ShowError(IosLocalizator.Translate(Constants.MESSAGE_CHECK_CONNECTION));
                }
				
				
            }

            private UIImage GetQRCode (string url)
			{
				var writer = new ZXing.Mobile.BarcodeWriter {
					Format = BarcodeFormat.QR_CODE,
					Options = new ZXing.Common.EncodingOptions {
						Height = 250,
						Width = 250
					}
				};
                return writer.Write (url);
			}
		}


    }
}

