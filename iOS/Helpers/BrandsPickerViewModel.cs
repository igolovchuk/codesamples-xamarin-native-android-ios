using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinNativeFullDemo.iOS.ViewModels
{
    public class BrandsPickerViewModel : UIPickerViewModel
    {
		private List<string> _myItems;
		protected int selectedIndex = 0;

		public BrandsPickerViewModel(List<string> items)
		{
			_myItems = items;
		}

		public string SelectedItem
		{
			get { return _myItems[selectedIndex]; }
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return _myItems.Count;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return _myItems[(int)row];
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			selectedIndex = (int)row;
		}
    }
}
