using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Plugin.LocalNotification;

namespace ritegeapp.Droid
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = false, NoHistory = true, Icon = "@mipmap/icon", Label = "Ritege Parking")]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "X:" + typeof (SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup ()
        {
            var data = Intent.GetStringExtra(LocalNotificationCenter.ReturnRequest);
            var mainintent = new Intent(Application.Context, typeof(MainActivity));
             mainintent.SetFlags(ActivityFlags.SingleTop);
            if (!string.IsNullOrWhiteSpace(data))
            {
                mainintent.PutExtra(LocalNotificationCenter.ReturnRequest, data);
            }
            StartActivity(mainintent);

        }
    }
}