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
    public partial class ClothesVC : UIViewController
    {
        private List<ClothesItem> _clothesItems;
		private List<ClothesItem> _filteredClothesItems;
        private IClothesService _clothesService;
        private IBrandService _brandService;
        //navigation
        private UINavigationItem _navigationItem;
        private UIBarButtonItem _editButton;
        private UIBarButtonItem _saveButton;

		public ClothesVC(IntPtr handle) : base(handle)
        {
            _clothesService = new ClothesService(new IosFileSystem());
			_clothesItems = _clothesService.GetAll();
			_filteredClothesItems = new List<ClothesItem>();
		}
      
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var clothesSource = new ClothesSource(this);
			tableView.Source = clothesSource;

           
			var sdc = searchDisplayController;
			sdc.SearchResultsSource = clothesSource;
            sdc.SearchResultsTableView.RowHeight = tableView.RowHeight;
			sdc.SearchBar.TextChanged += (sender, e) =>
				{
					string text = e.SearchText.ToLower();
                _filteredClothesItems = _clothesItems.Where(ci => IosLocalizator.Translate($"CLOTHES_{ci.Name.ToUpper().Replace("-", "_")}").ToLower().Contains(text)).ToList();
				};

            //init navBar items
            _navigationItem = navBar.Items[0];
            _navigationItem.Title = _brandService.Get(AppSettings.SelectedBrandId).Name;
            _editButton = _navigationItem.RightBarButtonItem;
            _saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, (s, e) => Save());
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		partial void BtnBack_Activated(UIBarButtonItem sender)
		{
            PerformSegue(Constants.SEGUE_FROM_CLOTHES_TO_BRAND, this);
		}

        partial void BtnUpdate_Activated(UIBarButtonItem sender)
        {
            _navigationItem.SetRightBarButtonItem(_saveButton, true);
            tableView.Editing = true;
            tableView.ReloadData();
        }

        private void Save()
        {
            var newData = tableView.VisibleCells
                                   .Select(cell => CT1Text.GetCellData<ClothesItem>(cell));
            if(Validator.ValidateClothesTypes(newData)){
                _clothesService.Update(newData);
                tableView.Editing = false;
                _navigationItem.SetRightBarButtonItem(_editButton, true);
                tableView.ReloadData();
            }
            else {
                this.ShowError(IosLocalizator.Translate(Constants.MESSAGE_UNEXISTING_SIZE));
            }

        }

        public void SetBrandService(IBrandService service)
		{
			_brandService = service;
		}

        //data source
        private class ClothesSource : UITableViewSource
		{
            private ClothesVC clothesController;
			private UISearchDisplayController search;

            public ClothesSource(ClothesVC clothesController)
			{
				this.clothesController = clothesController;
				search = clothesController.searchDisplayController;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
                tableview.RegisterNibForCellReuse(CT1Text.Nib, CT1Text.Key);
				return GetClothesItem().Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{				
                ClothesItem clothesItem = GetClothesItem()[indexPath.Row];
				var cell = (CT1Text)tableView.DequeueReusableCell(CT1Text.Key);
                cell.SetCellData(clothesItem.TypeId, clothesItem.Name, clothesItem.Icon, clothesItem.Size, tableView.Editing);
				return cell;
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
                return false;
			}
			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{

			}

            private List<ClothesItem> GetClothesItem()
			{
				return search.Active ? clothesController._filteredClothesItems : clothesController._clothesItems;
			}
		}
    }
}

