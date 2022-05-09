using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitegeDomain.Model
{
    public class ParkingEvent
    {
        public ParkingEvent()
        {
        }

        public ParkingEvent(string type, string description, DateTime date)
        {
            this.TypeEvent = type;
            this.DescriptionEvent = description;
            this.DateEvent = date;
        }
        public ParkingEvent(int ParkingId,string type, string description, DateTime date)
        {
            this.TypeEvent = type;
            this.ParkingId = ParkingId;
            this.DescriptionEvent = description;
            this.DateEvent = date;
        }

        public int ParkingId { get; set; }
        public string TypeEvent { get; set; }
        public string DescriptionEvent { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
