// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamarinNativeFullDemo.iOS.Controllers
{
    [Register ("ClothesVC")]
    partial class ClothesVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnUpdate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UINavigationBar navBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar searchBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchDisplayController searchDisplayController { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableView { get; set; }

        [Action ("BtnBack_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnBack_Activated (UIKit.UIBarButtonItem sender);

        [Action ("BtnUpdate_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnUpdate_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnUpdate != null) {
                btnUpdate.Dispose ();
                btnUpdate = null;
            }

            if (navBar != null) {
                navBar.Dispose ();
                navBar = null;
            }

            if (searchBar != null) {
                searchBar.Dispose ();
                searchBar = null;
            }

            if (searchDisplayController != null) {
                searchDisplayController.Dispose ();
                searchDisplayController = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }
        }
    }
}