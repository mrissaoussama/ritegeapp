using SQLite;
using System;

namespace RitegeDomain.Model
{
    public class NotificationToken
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Token { get; set; }
        public DateTime Date { get; set; }
        public NotificationToken(string t)
        {
            Token = t;
            Date = DateTime.Now;

        }

        public NotificationToken(int iD, string token, DateTime date)
        {
            ID = iD;
            Token = token;
            Date = date;
        }

        public NotificationToken()
        {
        }
    }
}
