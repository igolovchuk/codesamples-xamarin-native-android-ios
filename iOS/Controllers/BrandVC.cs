using System;
using XamarinNativeFullDemo.Extensions;
using System.Collections.Generic;
using XamarinNativeFullDemo.Models;
using XamarinNativeFullDemo.Interfaces;
using XamarinNativeFullDemo.iOS.Cells;
using XamarinNativeFullDemo.Services;
using XamarinNativeFullDemo.Data;
using UIKit;
using Foundation;
using System.Linq;
using XamarinNativeFullDemo.iOS.Helpers;

namespace XamarinNativeFullDemo.iOS.Controllers
{
	public partial class BrandVC : UIViewController
	{
        private List<Brand> _brands;
        private List<Brand> _filteredBrands;
		private IBrandService _brandService;
        private IAccountService _accountService;

		public BrandVC(IntPtr handle) : base(handle)
		{
            _brandService = new BrandService(new IosFileSystem());
			_brands = _brandService.GetAll();
			_filteredBrands = new List<Brand>();      
		}

        public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
            var brandSource = new BrandSource(this);
            tableView.Source = brandSource;

            navBar.Items[0].Title = AppSettings.SelectedAccount.ToFullName();
			var sdc = searchDisplayController;
            sdc.SearchResultsSource = brandSource;
            
            sdc.SearchResultsTableView.RowHeight = tableView.RowHeight;
			sdc.SearchBar.TextChanged += (sender, e) =>
				{
                string text = e.SearchText.ToLower();
                _filteredBrands = _brands.Where(b => b.Name.ToLower().Contains(text)).ToList();
				};
			_accountService = _accountService ?? new AccountService(new IosFileSystem());
			// Perform any additional setup after loading the view, typically from a nib.
		}



		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			// do first a control on the Identifier for your segue
            if (segue.Identifier.Equals(Constants.SEGUE_FROM_BRAND_TO_CLOTHES))
			{
                var viewController = (ClothesVC)segue.DestinationViewController;
                viewController.SetBrandService(_brandService);
			}
		}
		partial void btnEditAcoount_Click(UIBarButtonItem sender)
		{
			UIStoryboard board = UIStoryboard.FromName("Main", null);
            CalculationVC ctrl = (CalculationVC)board.InstantiateViewController("CalcVC");

            var account = _accountService.Get(AppSettings.SelectedAccount);
            ctrl.SetAccountService(_accountService);
            ctrl.SetAccount(account);
            ctrl.IsEditMode = true;
            
            ShowViewController(ctrl, this);
		}

		partial void btnBack_Click(UIBarButtonItem sender)
		{
            PerformSegue(Constants.SEGUE_FROM_BRAND_TO_ACCOUNT, this);
		}

        public void SetAccountService(IAccountService service){
            _accountService = service;
        }

        //data source
     private class BrandSource : UITableViewSource
      {
        private BrandVC brandController;
        private UISearchDisplayController search;

        public BrandSource(BrandVC brandController)
        {
            this.brandController = brandController;
            search = brandController.searchDisplayController;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
           tableview.RegisterNibForCellReuse(CT1.Nib, CT1.Key);
            return GetBrand().Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {          
	        Brand brand = GetBrand()[indexPath.Row];
            var cell = ( CT1)tableView.DequeueReusableCell( CT1.Key);
            cell.SetCellData(brand.Name, brand.Icon);
            return cell;
        }
             
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
                AppSettings.SelectedBrandId = GetBrand()[indexPath.Row].Id; 
                brandController.PerformSegue(Constants.SEGUE_FROM_BRAND_TO_CLOTHES, brandController);
        }

        private List<Brand> GetBrand()
        {
            return search.Active ? brandController._filteredBrands : brandController._brands;
        }
     }
   }
}
