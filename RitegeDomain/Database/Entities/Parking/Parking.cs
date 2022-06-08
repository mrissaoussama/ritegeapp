using System;
using System.Collections.Generic;
using System.Text;

namespace RitegeDomain.Database.Entities.Parking
{
    public class Parking:IEntity
    {
        public int IdParking { get; set; }
        public DateTime DateHeureSauvegarde { get; set;  }
        public string FluxPayment { get; set; }
        public string NomParking { get; set; }
        public int PlacesOccupees { get; set; }
        public int CapaciteParking { get; set; }
        public int IdSociete { get; set; }
    }
}
