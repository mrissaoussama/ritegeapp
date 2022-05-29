using System;

namespace RitegeDomain.DTO
{
    public enum TypeAbonnementEnum { Interval, Annuel, Semestriel, Trimestriel, Hebdomadaire, Journalier,Jour,Mensuel };
    public enum Etat { Future, Archivé, Activé }
    public class InfoAbonnementDTO:IEntity
    {
        private string _LibelleAbonnement, _nomPrenomAbonne;
        private TypeAbonnementEnum? _typeAbonnement;
        private Decimal _PrixAbonnement;
        private Etat _etat;
        private DateTime _dateAffectation, _dateFinActivation, _DateActivation;

        public string LibelleAbonnement { get => _LibelleAbonnement; set => _LibelleAbonnement = value; }
        public Etat Etat { get => _etat; set => _etat = value; }

        public string NomPrenomAbonne { get => _nomPrenomAbonne; set => _nomPrenomAbonne = value; }
        public TypeAbonnementEnum? TypeAbonnement { get => _typeAbonnement; set => _typeAbonnement = value; }
        public decimal PrixAbonnement { get => _PrixAbonnement; set => _PrixAbonnement = value; }
        public DateTime DateAffectation { get => _dateAffectation; set => _dateAffectation = value; }
        public DateTime DateFinActivation { get => _dateFinActivation; set => _dateFinActivation = value; }
        public DateTime DateActivation { get => _DateActivation; set => _DateActivation = value; }

        public InfoAbonnementDTO()
        {
        }

        public InfoAbonnementDTO(string libelleAbonnement, string nomPrenomAbonne, TypeAbonnementEnum typeAbonnement, decimal prixAbonnement, DateTime dateAffectation, DateTime dateActivation, DateTime dateFinActivation)
        {
            LibelleAbonnement = libelleAbonnement;
            NomPrenomAbonne = nomPrenomAbonne;
            this.TypeAbonnement = typeAbonnement;
            PrixAbonnement = prixAbonnement;
            DateAffectation = dateAffectation;
            DateFinActivation = dateFinActivation;
            DateActivation = dateActivation;
        }

        public InfoAbonnementDTO(string nomPrenomAbonne, DateTime dateFinActivation, DateTime dateActivation)
        {
            NomPrenomAbonne = nomPrenomAbonne;
            DateFinActivation = dateFinActivation;
            DateActivation = dateActivation;
        }

        public InfoAbonnementDTO(string nomPrenomAbonne, TypeAbonnementEnum typeAbonnement, DateTime dateFinActivation, DateTime dateActivation)
        {
            NomPrenomAbonne = nomPrenomAbonne;
            TypeAbonnement = typeAbonnement;
            DateFinActivation = dateFinActivation;
            DateActivation = dateActivation;
        }

        public InfoAbonnementDTO(string libelleAbonnement,
            Etat etat, string nomPrenomAbonne, TypeAbonnementEnum typeAbonnement,
            decimal prixAbonnement,
            DateTime dateAffectation, DateTime dateFinActivation, DateTime dateActivation)
        {
            LibelleAbonnement = libelleAbonnement;
            Etat = etat;
            NomPrenomAbonne = nomPrenomAbonne;
            TypeAbonnement = typeAbonnement;
            PrixAbonnement = prixAbonnement;
            DateAffectation = dateAffectation;
            DateFinActivation = dateFinActivation;
            DateActivation = dateActivation;
        }
    }
}
