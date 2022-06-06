using Android.App;
using Microsoft.AspNetCore.SignalR.Client;
using ritegeapp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ritegeapp.Droid.Services
{
    [Service]
    public class ForegroundNotificationService:Service
    {
        public static HubConnection hubConnection;
        public static HubConnection GetHub()
        {
            return hubConnection;
        }
        public static void SetHub(HubConnection hub)
        {
            hubConnection = hub;
        }
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            return null;
        }
        public const int ServiceRunningNotifID = 1;
        public override Android.App.StartCommandResult OnStartCommand(Android.Content.Intent intent, Android.App.StartCommandFlags flags, int startId)
        {
            Android.App.Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            StartForeground(ServiceRunningNotifID, notif);
            hubConnection =DependencyService.Get<ISignalRService>().GetHub();
            return Android.App.StartCommandResult.Sticky;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Android.Content.Intent name)
        {
            return base.StopService(name);
        }
    }
}
