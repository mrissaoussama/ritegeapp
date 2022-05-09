using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using ritegeapp.Droid;
using ritegeapp.Droid.Services;
using ritegeapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(AndroidServiceHelper))]
namespace ritegeapp.Droid
{
    public class AndroidServiceHelper:IEventService
    {
        private static Context context = global::Android.App.Application.Context;
        public HubConnection GetHub()
        {
            return ForegroundNotificationService.GetHub();
        }
        public  void SetHub(HubConnection hub) => ForegroundNotificationService.SetHub(hub);

        public bool IsStarted { get; set; }

        public void StartService()
        {

           var intent = new Intent(context, typeof(ForegroundNotificationService));
//            var intent = new Android.Content.Intent(context, new ForegroundNotificationService().Class);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }

        public void StopService()
        {
            var intent = new Intent(context, typeof(DataService));
            context.StopService(intent);
        }
    }
}