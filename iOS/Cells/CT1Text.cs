using System;
using System.Text.RegularExpressions;
using Foundation;
using XamarinNativeFullDemo.iOS.Helpers;
using UIKit;

namespace XamarinNativeFullDemo.iOS.Cells
{
    public partial class CT1Text : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CT1Text");
        public static readonly UINib Nib;
		public static readonly int Height;
        private string removeRegex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";

        static CT1Text()
        {
            Nib = UINib.FromName("CT1Text", NSBundle.MainBundle);
            Height = 64;
        }

        public void SetCellData(string typeId, string name, string iconPath, string size, bool isEditMode)
		{
            lblId.Text = typeId;
            lblName.Text = IosLocalizator.Translate($"CLOTHES_{name.ToUpper().Replace("-","_")}");

			imgLogo.Image = UIImage.FromFile(iconPath);
			imgLogo.Layer.BorderWidth = 1;
			imgLogo.Layer.BorderColor = UIColor.FromRGB(51, 109, 97).CGColor;

            txtSize.Enabled = isEditMode;
            txtSize.BorderStyle = isEditMode ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
            if(isEditMode){
				txtSize.Layer.BorderWidth = 1;
                txtSize.Layer.BorderColor = UIColor.FromRGB(51,109,97).CGColor;
                txtSize.Layer.CornerRadius = 5;
            }

            txtSize.Text = isEditMode ? Regex.Replace(size, removeRegex, "") : size;
		}

        public static T GetCellData<T>(UITableViewCell tableCell)
		{
            var cell = (CT1Text)tableCell;
            return  (T)Activator.CreateInstance(typeof(T), new object[] { cell.lblId.Text, cell.lblName.Text, string.Empty, cell.txtSize.Text } );
		}

        protected CT1Text(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
