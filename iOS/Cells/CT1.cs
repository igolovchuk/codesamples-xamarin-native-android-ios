using System;

using Foundation;
using UIKit;

namespace XamarinNativeFullDemo.iOS.Cells
{
    public partial class  CT1 : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CT1");
        public static readonly UINib Nib;
        public static readonly int Height;

        static  CT1()
        {
            Nib = UINib.FromName("CT1", NSBundle.MainBundle);
            Height = 64;
        }
    
        public void SetCellData(string name, string iconPath)
        {
            lblName.Text = name;
            imgLogo.Image = UIImage.FromFile(iconPath);
            imgLogo.Layer.BorderWidth = 1;
            imgLogo.Layer.BorderColor = UIColor.FromRGB(51, 109, 97).CGColor;
        }

        protected  CT1(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
