// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Entities.Parking
{
    // evenement
    public class Evenement : IEntity
    {
        public long IdEvenement { get; set; } // idEvenement (Primary key)
        public DateTime DateEvent { get; set; } // dateEvent
        public string DescriptionEvent { get; set; } // descriptionEvent (length: 1000)
        public string TypeEvent { get; set; } // typeEvent (length: 100)
        public int? idCaisse { get; set; } // idCaisse
        public int? idBorne { get; set; } // idBorne
        public string LogCaissier { get; set; } // logCaissier (length: 100)
    }

}
// </auto-generated>
