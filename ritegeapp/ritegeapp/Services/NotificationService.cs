using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
using ritegeapp.Utils;
using RitegeDomain.DTO;
using RitegeDomain.Model;
using Xamarin.Forms;

namespace ritegeapp.Services
{
    public class NotificationService : INotificationService
    {
        public int NotificationID = 3;

        public NotificationService()
        {
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                LocalNotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            DependencyService.Register<IMessage>();

        }
        public void CreateAlertNotification(EventDTO parkingEvent)
        { 
            var notification = new NotificationRequest
            {
              
                NotificationId = NotificationID,
                Title = "Parking Alert",
                Description = parkingEvent.DescriptionEvent,
                ReturningData = "Alert Tapped",
                Android =
                {
                Priority =AndroidPriority.Max,
            IconSmallName= {
              ResourceName = "alert",
        }
                }
            };
            NotificationID++;
            notification.Android.ChannelId = "Alert_Channel";
            _ = LocalNotificationCenter.Current.Show(notification);
        }
        private async void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            if (e.Request is null)
            {
                return;
            }

        }
    }
}
