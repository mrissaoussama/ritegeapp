using System;

namespace RitegeDomain.Model
{
    public class NotificationToken
    {
        public string Token { get; set; }
        public DateTime Date { get; set; }
        public NotificationToken(string t)
        {
            Token = t;
            Date = DateTime.Now;

        }


        public NotificationToken()
        {
        }
    }
}
