// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Entities.Parking
{
    // ticket
    public class Ticket : IEntity
    {
        public long IdTicket { get; set; } // idTicket (Primary key)
        public DateTime DateHeureDebutStationnement { get; set; } // dateHeureDebutStationnement
        public DateTime? DateHeureFinStationnement { get; set; } // dateHeureFinStationnement
        public string EtatTicket { get; set; } // etatTicket (length: 100)
        public int? IdTarifTicket { get; set; } // idTarifTicket
        public decimal? Tarif { get; set; } // Tarif
        public int idBorneEntree { get; set; } // idBorneEntree
        public int? idBorneSortie { get; set; } // idBorneSortie
        public string LogCaissier { get; set; } // LogCaissier (length: 100)
        public bool? AvecTarif2 { get; set; } // avecTarif2

        public Ticket()
        {
        }
    }

}
// </auto-generated>