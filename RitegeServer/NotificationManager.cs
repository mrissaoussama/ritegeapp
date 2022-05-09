//using Newtonsoft.Json.Linq;
//using PushSharp.Google;

//namespace RitegeServer
//{
//    public static class NotificationManager
//    {
      
//        static readonly string provider = "FCM";
//        public static List<string> Tokens { get; set; }
//        static NotificationManager()
//        {
//            Tokens = new();
//            config = new GcmConfiguration("AAAAuuQM54o:APA91bEOjRbZLvfX-NInpJ_-t8SJ1vS_oD_l_14YmEW-YAi21Ay8ssWkO-TQBrpy-UehxykvyPxBuWa-HTx77fguYobLWlWZjrShtC8k5cDhK6l5iitydy0jr-lb1uzFfl13FmOEu0bq ");
//            config.GcmUrl = "https://fcm.googleapis.com/fcm/send";
//            gcmBroker = new GcmServiceBroker(config);

//            // Start the broker
//            gcmBroker.Start();
//        }
//        public static void SendDelayedNotification(List<string> tokens, string key, string value, string eventtype)
//        {
//            Thread.Sleep(5000);
//            foreach (var regId in tokens)
//            {
//                // Queue a notification to send
//                gcmBroker.QueueNotification(new GcmNotification
//                {
//                    RegistrationIds = Tokens,
//                    Priority = GcmNotificationPriority.High,
//                    Data = JObject.FromObject(new
//                    {
//                        title = key,
//                        body = value,
//                    }),
//                    Notification = JObject.FromObject(new
//                    {
//                        title = key,
//                        body = value,

//                    }),
//                });
//            }

//        }
//        public static void SendNotification(List<string> tokens, string key, string value, string eventtype)
//        {
//            foreach (var regId in tokens)
//            {
//                // Queue a notification to send
//                gcmBroker.QueueNotification(new GcmNotification
//                {
//                    RegistrationIds = Tokens,
//                    Priority = GcmNotificationPriority.High,
//                    Data = JObject.FromObject(new
//                    {
//                        title = key,
//                        body = value,
//                    }),
//                    Notification = JObject.FromObject(new
//                    {
//                        title = key,
//                        body = value,

//                    }),
//                });
//            }

//        }
//    }
//}
