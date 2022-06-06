using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using ritegeapp.Utils;
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
        public void CreateAlertNotification(ParkingEvent parkingEvent)
        {
            var notification = new NotificationRequest
            {
                //Android= androidoptions,
                NotificationId = NotificationID,
                Title = "Parking Alert",
                Description = parkingEvent.DescriptionEvent,
                ReturningData = "Alert Tapped",

            };
            NotificationID++;
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
