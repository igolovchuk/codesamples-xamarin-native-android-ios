
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinNativeFullDemo.Data;

namespace XamarinNativeFullDemo.Droid.Activities
{
    [Activity(Label = "AdvertActivity")]
    public class AdvertActivity : Activity
    {
        private AdView mAdView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            InitAdBanner();
        }


        #region adview

        private void InitAdBanner()
        {
            mAdView = FindViewById<AdView>(Resource.Id.adView);
            if(mAdView != null){
				mAdView.Visibility = ViewStates.Gone;
				mAdView.Destroy();  
            }

        }

        protected override void OnPause()
        {
            if (mAdView != null)
            {
                mAdView.Pause();
            }
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mAdView != null)
            {
                mAdView.Resume();
            }
        }

        protected override void OnDestroy()
        {
            if (mAdView != null)
            {
                mAdView.Destroy();
            }
            base.OnDestroy();
        }
        #endregion
    }
}
