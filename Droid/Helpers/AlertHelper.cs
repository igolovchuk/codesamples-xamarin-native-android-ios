using System;
using Android.App;
using Android.Graphics;
using Android.Widget;
using XamarinNativeFullDemo.Data;

namespace XamarinNativeFullDemo.Droid.Helpers
{
    public static class AlertHelper
    {
        public static void ShowError(this Activity scope, string message)
        {
            ShowOneButtonAlert(scope, AndroidLocalizator.Translate(Constants.TITLE_ERROR), message);
        }

        public static void ShowInfo(this Activity scope, string message)
        {
            ShowOneButtonAlert(scope, AndroidLocalizator.Translate(Constants.TITLE_INFO), message);
        }

        public static void ShowWarning(this Activity scope, string message)
        {
            ShowOneButtonAlert(scope, AndroidLocalizator.Translate(Constants.TITLE_DELETE), message);
        }

        private static void ShowOneButtonAlert(Activity scope, string type, string message)
        {
			var custom_title = new TextView(scope);
            custom_title.Text = type;
			custom_title.SetBackgroundColor(Color.Rgb(51, 109, 97));
			custom_title.SetPadding(10, 10, 10, 10);
			custom_title.Gravity = Android.Views.GravityFlags.Center;
			custom_title.SetTextColor(Color.White);
			custom_title.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);

           var builder = new AlertDialog.Builder(scope)
             .SetNeutralButton(Resource.String.ok, (s, ev) => { })
             .SetMessage(message)
             .SetCustomTitle(custom_title)
             .Show();

			builder.Window.SetBackgroundDrawableResource(Resource.Drawable.solid_shape);
			//builder.Window.SetBackgroundDrawableResource(Resource.Color.windowBackground);

			var button = builder.FindViewById<Button>(Android.Resource.Id.Button3);
			button.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
			button.LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent);
			button.SetBackgroundResource(Resource.Drawable.single_border);
			button.Gravity = Android.Views.GravityFlags.CenterHorizontal;
        }

        public static void ShowOkCancelAlert(this Activity scope, string type, string message, Action okHandler)
        {           
            var custom_title = new TextView(scope);
            custom_title.Text = type;
            custom_title.SetBackgroundColor(Color.Rgb(51, 109, 97));
            custom_title.SetPadding(10, 10, 10, 10);
            custom_title.Gravity = Android.Views.GravityFlags.Center;
            custom_title.SetTextColor(Color.White);
            custom_title.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);

			var builder = new AlertDialog.Builder(scope)
			  .SetPositiveButton(Resource.String.ok, (s, e) => scope.RunOnUiThread(okHandler))
			  .SetNegativeButton(Resource.String.cancel, (s, e) => { })
			  .SetMessage(message)
			  .SetCustomTitle(custom_title)
			  .Show();

			builder.Window.SetBackgroundDrawableResource(Resource.Drawable.solid_shape);
            //builder.Window.SetBackgroundDrawableResource(Resource.Color.windowBackground);

			var messageText = builder.FindViewById<TextView>(Android.Resource.Id.Message);
			messageText.Gravity = Android.Views.GravityFlags.Center;

            var btnwidth = (int)(scope.Resources.DisplayMetrics.WidthPixels / 2 - 40 * scope.Resources.DisplayMetrics.Density);
			var buttonCancel = builder.FindViewById<Button>(Android.Resource.Id.Button2);
			buttonCancel.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
            buttonCancel.LayoutParameters = new LinearLayout.LayoutParams(btnwidth, Android.Views.ViewGroup.LayoutParams.WrapContent);
            buttonCancel.SetBackgroundResource(Resource.Drawable.right_button_border);
            buttonCancel.Gravity = Android.Views.GravityFlags.Center;

			var buttonOk = builder.FindViewById<Button>(Android.Resource.Id.Button1);
			buttonOk.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
            buttonOk.LayoutParameters = new LinearLayout.LayoutParams(btnwidth, Android.Views.ViewGroup.LayoutParams.WrapContent);
            buttonOk.Gravity = Android.Views.GravityFlags.Center;
           // new AlertDialog.Builder(scope)
           //     .SetPositiveButton(Resource.String.ok, (s, e) => scope.RunOnUiThread(okHandler))
           //     .SetNegativeButton(Resource.String.cancel, (s, e) => { })
           //     .SetMessage(message)
           //     .SetTitle(type)
           //     .Show();
        }

        public static void ShowImageAlert(this Activity scope, string title, Bitmap image, string message)
        {
            var imageView = new ImageView(scope);
            imageView.SetAdjustViewBounds(true);
            imageView.SetImageBitmap(image);

            var custom_title = new TextView(scope);
            custom_title.Text = title;
            custom_title.SetBackgroundColor(Color.Rgb(51, 109, 97));
            custom_title.SetPadding(10, 10, 10, 10);
            custom_title.Gravity = Android.Views.GravityFlags.Center;
            custom_title.SetTextColor(Color.White);
            custom_title.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);

            var builder = new AlertDialog.Builder(scope)
           .SetNeutralButton(Resource.String.ok, (s, ev) => { })
           .SetMessage(message)
           .SetCustomTitle(custom_title)
           .SetView(imageView)
           .Show();

			builder.Window.SetBackgroundDrawableResource(Resource.Drawable.solid_shape);
			//builder.Window.SetBackgroundDrawableResource(Resource.Color.windowBackground);

            var messageText = builder.FindViewById<TextView>(Android.Resource.Id.Message);
            messageText.Gravity = Android.Views.GravityFlags.Center;

            var button = builder.FindViewById<Button>(Android.Resource.Id.Button3);
            button.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
            button.LayoutParameters = new LinearLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.WrapContent);
            button.SetBackgroundResource(Resource.Drawable.single_border);
            button.Gravity = Android.Views.GravityFlags.CenterHorizontal;
        }

        public static void ShowTwoButtonAlert(this Activity scope, string title, int firstButton, int secondButton, Action firstHandler, Action secondHandler){
			var custom_title = new TextView(scope);
			custom_title.Text = title;
			custom_title.SetBackgroundColor(Color.Rgb(51, 109, 97));
			custom_title.SetPadding(10, 10, 10, 10);
			custom_title.Gravity = Android.Views.GravityFlags.Center;
			custom_title.SetTextColor(Color.White);
			custom_title.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);

			var builder = new AlertDialog.Builder(scope)
			  .SetNegativeButton(AndroidLocalizator.Translate(firstButton), (s, e) => scope.RunOnUiThread(firstHandler))
              .SetPositiveButton(AndroidLocalizator.Translate(secondButton), (s, e) => scope.RunOnUiThread(secondHandler))
			  .SetCustomTitle(custom_title)
			  .Show();
            
            builder.Window.SetBackgroundDrawableResource(Resource.Drawable.solid_shape);

			var btnwidth = (int)(scope.Resources.DisplayMetrics.WidthPixels / 2 - 40 * scope.Resources.DisplayMetrics.Density);
			var buttonFirst = builder.FindViewById<Button>(Android.Resource.Id.Button2);
			buttonFirst.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
			buttonFirst.LayoutParameters = new LinearLayout.LayoutParams(btnwidth, Android.Views.ViewGroup.LayoutParams.WrapContent);
            buttonFirst.SetBackgroundResource(Resource.Drawable.right_button_border);
            buttonFirst.Gravity = Android.Views.GravityFlags.Center;

			var buttonSecond = builder.FindViewById<Button>(Android.Resource.Id.Button1);
			buttonSecond.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
			buttonSecond.LayoutParameters = new LinearLayout.LayoutParams(btnwidth, Android.Views.ViewGroup.LayoutParams.WrapContent);
			
            buttonSecond.Gravity = Android.Views.GravityFlags.Center;
        }

        public static ProgressDialog ShowProgress(this Activity scope){
            var mDialog = new ProgressDialog(scope);
            mDialog.SetMessage(AndroidLocalizator.Translate(Resource.String.MESSAGE_LOAD_DATA));
				mDialog.SetCancelable(false);
				mDialog.Show();

            return mDialog;
        }
    }
}
