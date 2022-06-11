using System;

namespace RitegeDomain.DTO
{

    public class EventDTO : IEntity
    {
        public EventDTO() { }
        public DateTime DateEvent { get; set; } // dateEvent
        public long IndexEvent { get; set; } // indexEvent (Primary key)
        public string HeureEvent { get; set; } // HeureEvent (length: 8)
        public string DescriptionEvent { get; set; } // HeureEvent (length: 8)
        public ushort DoorNumber { get; set; } // DoorNumber
        public ushort? UserNumber { get; set; } // userNumber
        public ushort CodeEvent { get; set; } // codeEvent
        public ushort? Flux { get; set; } // Flux
    }
    }
