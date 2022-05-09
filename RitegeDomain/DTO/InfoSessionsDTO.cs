using System;
using System.Collections.Generic;
using System.Text;

namespace RitegeDomain.DTO
{
    public class InfoSessionsDTO:IEntity
    {
        string _Caissier, _caisse;

        Decimal _recette;
        DateTime _dateStartSession, _dateEndSession;
        int _index, _nbTickets, _nbAutorite, _nbEgress, _nbAdministratif, _nbAbonne;

        public InfoSessionsDTO(string Caissier, string caisse, DateTime dateStartSession, DateTime dateEndSession)
        {
            this.Caissier = Caissier;
            Caisse = caisse;
            DateStartSession = dateStartSession;
            DateEndSession = dateEndSession;
        }
        public InfoSessionsDTO()
        {
        }
        public string Caissier { get => _Caissier; set => _Caissier = value; }
        public string Caisse { get => _caisse; set => _caisse = value; }
        public decimal Recette { get => _recette; set => _recette = value; }
        public DateTime DateStartSession { get => _dateStartSession; set => _dateStartSession = value; }
        public DateTime DateEndSession { get => _dateEndSession; set => _dateEndSession = value; }
        public int Index { get => _index; set => _index = value; }
        public int NbTickets { get => _nbTickets; set => _nbTickets = value; }
        public int NbAutorite { get => _nbAutorite; set => _nbAutorite = value; }
        public int NbEgress { get => _nbEgress; set => _nbEgress = value; }
        public int NbAdministratif { get => _nbAdministratif; set => _nbAdministratif = value; }
        public int NbAbonne { get => _nbAbonne; set => _nbAbonne = value; }
        public override string ToString()
        {
            return Caissier;
        }
    }
}

