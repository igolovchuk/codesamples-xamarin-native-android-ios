// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinNativeFullDemo.iOS.Cells
{
    [Register ("CT1Text")]
    partial class CT1Text
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblId { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtSize { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgLogo != null) {
                imgLogo.Dispose ();
                imgLogo = null;
            }

            if (lblId != null) {
                lblId.Dispose ();
                lblId = null;
            }

            if (lblName != null) {
                lblName.Dispose ();
                lblName = null;
            }

            if (txtSize != null) {
                txtSize.Dispose ();
                txtSize = null;
            }
        }
    }
}