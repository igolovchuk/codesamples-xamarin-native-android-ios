using System;
using Foundation;
using XamarinNativeFullDemo.Data;
using UIKit;

namespace XamarinNativeFullDemo.iOS.Helpers
{
    public static class AlertHelper
    {
        public static void ShowError(this UIViewController scope, string message){
            ShowOneButtonAlert(scope, IosLocalizator.Translate(Constants.TITLE_ERROR), message);
        }

		public static void ShowInfo(this UIViewController scope, string message)
		{
            ShowOneButtonAlert(scope, IosLocalizator.Translate(Constants.TITLE_INFO), message);
		}

        public static void ShowWarning(this UIViewController scope, string message)
		{
            ShowOneButtonAlert(scope, IosLocalizator.Translate(Constants.TITLE_DELETE), message);
		}

        private static void ShowOneButtonAlert(UIViewController scope, string type, string message){
			var okAlertController = UIAlertController.Create(type, message, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_OK), UIAlertActionStyle.Default, null));
			scope.PresentViewController(okAlertController, true, null); 
        }

        public static void ShowOkCancelAlert(this UIViewController scope, string type, string message, Action okHandler)
		{
			var okCancelAlertController = UIAlertController.Create(type, message, UIAlertControllerStyle.Alert);
          
			//Add Actions
            okCancelAlertController.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_OK), UIAlertActionStyle.Default, alert => scope.Invoke(okHandler, 0)));
            okCancelAlertController.AddAction(UIAlertAction.Create(IosLocalizator.Translate(Constants.BUTTON_CANCEL), UIAlertActionStyle.Cancel, null));

			//Present Alert
			scope.PresentViewController(okCancelAlertController, true, null);
		}

        public static void ShowImageAlert(this UIViewController scope, string title, UIImage image, string message){
			var alert = new UIAlertView () {
                Title = title,
                Message = message
			};
            alert.SetValueForKey (new UIImageView (image), (NSString)"accessoryView");
			alert.AddButton (IosLocalizator.Translate (Constants.BUTTON_OK));
			alert.Show ();
		}
    }
}
