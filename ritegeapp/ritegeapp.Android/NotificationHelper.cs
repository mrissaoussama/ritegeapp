using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using ritegeapp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]

namespace ritegeapp.Droid
{
    internal class NotificationHelper : INotification
    {
        private static string foregroundChannelId = "2";
        private static Context context = global::Android.App.Application.Context;


        public Notification ReturnNotif()
        {
            // Building intent
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);
          
            var notifBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle("Service Parking")
                .SetContentText("Service évenenements dangereux activé")
                .SetSmallIcon(Resource.Drawable.alerticon)
                .SetOngoing(true)
                .SetSound(null)
                .SetContentIntent(pendingIntent);

            // Building channel if API verion is 26 or above
            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var audioattributes = new AudioAttributes.Builder();
                audioattributes.SetContentType(AudioContentType.Music);
                audioattributes.SetUsage(AudioUsageKind.Notification);
                NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Alarm", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetSound(Android.Net.Uri.Parse("android.resource://" + AppInfo.PackageName + "/raw/notificationsound"), audioattributes.Build());

                var notifManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notifManager != null)
                {
                    notifBuilder.SetChannelId(foregroundChannelId);
                    notifManager.CreateNotificationChannel(notificationChannel);
                }
            }

            return notifBuilder.Build();
        }
    }
}