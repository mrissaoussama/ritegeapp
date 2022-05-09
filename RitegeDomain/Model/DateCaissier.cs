    using RitegeDomain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RitegeDomain.Model
{
    public class DateCaissier
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<InfoSessionsDTO> ListSessions { get; set; }
        public DateCaissier(IEnumerable<InfoSessionsDTO> list)
        {
            ListSessions = list.Where(x => x.DateStartSession == Date).ToList();
            Total = ListSessions.Sum(x => x.Recette);
        }
        public DateCaissier(IEnumerable<InfoSessionsDTO> list, DateTime d)
        {
            Date = d;
            ListSessions = list.Where(x => x.DateStartSession == Date).ToList();
            Total = ListSessions.Sum(x => x.Recette);
        }
        public int TicketTotal => ListSessions.Sum(x => x.NbTickets);
        public int AutoriteTotal => ListSessions.Sum(x => x.NbAutorite);
        public int AdministratifTotal => ListSessions.Sum(x => x.NbAdministratif);
        public int AbonneTotal => ListSessions.Sum(x => x.NbAbonne);
        public decimal RecetteTotal => ListSessions.Sum(x => x.Recette);
    }
}
