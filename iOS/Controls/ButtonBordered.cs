using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XamarinNativeFullDemo.iOS.Controls
{
   [Register("ButtonBordered"), DesignTimeVisible(true)]
    public class ButtonBordered : UIButton
    {
		[Export("BorderWidth"), Browsable(true)]
		public int BorderWidth { get; set; }

        public int CornerRadius { get; set; }
		
		public CGColor BorderColor { get; set; }

		public ButtonBordered(IntPtr handle) : base(handle)
        {
            Initialize();
			Layer.BorderWidth = BorderWidth;
			Layer.CornerRadius = CornerRadius;
			Layer.BorderColor = BorderColor;
		}

        private void Initialize(){
            BorderWidth = 1;
            CornerRadius = 4;
            BorderColor = UIColor.FromRGB(51, 109, 97).CGColor;
        }

    }
}
