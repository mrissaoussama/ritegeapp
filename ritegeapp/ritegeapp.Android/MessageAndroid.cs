using Android.App;
using Android.Widget;
using ritegeapp.Droid;
using ritegeapp.Utils;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace ritegeapp.Droid
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Debug.WriteLine("short alert");
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}