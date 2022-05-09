using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Linq;

namespace RitegeDomain.Model
{
    public class Caissier
    {
        public string NomCaissier { get; set; }
        public decimal Total { get; set; }
        public List<InfoSessionsDTO> ListSessions { get; set; }
        public Caissier(IEnumerable<InfoSessionsDTO> list)
        {
            ListSessions = list.Where(x => x.Caissier == NomCaissier).ToList();
            Total = ListSessions.Sum(x => x.Recette);
        }
        public Caissier(IEnumerable<InfoSessionsDTO> list, string nom)
        {
            NomCaissier = nom;
            ListSessions = list.Where(x => x.Caissier == NomCaissier).ToList();
            Total = ListSessions.Sum(x => x.Recette);
        }
        public int TicketTotal => ListSessions.Sum(x => x.NbTickets);
        public int AutoriteTotal => ListSessions.Sum(x => x.NbAutorite);
        public int AdministratifTotal => ListSessions.Sum(x => x.NbAdministratif);
        public int AbonneTotal => ListSessions.Sum(x => x.NbAbonne);
        public decimal RecetteTotal => ListSessions.Sum(x => x.Recette);
    }
}
