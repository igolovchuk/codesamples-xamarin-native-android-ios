// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinNativeFullDemo.iOS.Controllers
{
    [Register ("BrandVC")]
    partial class BrandVC
    {
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

        [Action ("btnBack_Click:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnBack_Click (UIKit.UIBarButtonItem sender);

        [Action ("btnEditAcoount_Click:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnEditAcoount_Click (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
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