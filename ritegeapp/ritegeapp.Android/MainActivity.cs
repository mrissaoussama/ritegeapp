using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.LocalNotification;
using ritegeapp.Droid;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ritegeapp.Droid
{
    [Activity(Label = "Ritege Parking", ClearTaskOnLaunch = false, FinishOnTaskLaunch = false, Icon = "@mipmap/icon", LaunchMode = LaunchMode.SingleTask, Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private event EventHandler Creating = delegate { };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTheme(Resource.Style.MainTheme);
            Window.AddFlags(Android.Views.WindowManagerFlags.ForceNotFullscreen);
            Window.ClearFlags(Android.Views.WindowManagerFlags.Fullscreen);
      
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Creating += onCreating;
            Creating(this, EventArgs.Empty);

            LocalNotificationCenter.CreateNotificationChannel();
            LoadApplication(new App());

            LocalNotificationCenter.NotifyNotificationTapped(Intent);
        }
        private async void onCreating(object sender, EventArgs args)
        {
            Creating -= onCreating;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#f08407"));
                });
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
      
        protected override void OnNewIntent(Intent intent)
        {
            LocalNotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }


    }
}