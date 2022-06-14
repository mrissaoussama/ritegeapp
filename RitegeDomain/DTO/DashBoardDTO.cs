using System;
using System.ComponentModel;

namespace RitegeDomain.DTO
{
    public enum Flux { Entree, Sortie }

    public class DashBoardDTO:IEntity
    {
        string? _parking, _caisse, _nomPrenomCaissier;
        bool? _etatCaisse;
        decimal? _recetteParking, _recetteCaissier, _recetteCaisse;
        //recetteCaissier: recette en cours, recetteCaisse:total journee
        Flux? _fluxBorne, _fluxCaisse;
        int? _nbTickets, _nbAdministrateur, _placeDisponible, _placeMax, _nbAutorite, _nbEgress, _nbAbonne, _fluxBorneTotal, _fluxCaisseTotal;

        public DashBoardDTO()
        {
        }

      
        public string? Parking { get => _parking; set => _parking = value; }
        public string? Caisse { get => _caisse; set => _caisse = value; }
        public string? NomPrenomCaissier { get => _nomPrenomCaissier; set => _nomPrenomCaissier = value; }
        public bool? EtatCaisse { get => _etatCaisse; set => _etatCaisse = value; }
        public decimal? RecetteParking { get => _recetteParking; set => _recetteParking = value; }
        public decimal? RecetteCaissier { get => _recetteCaissier; set => _recetteCaissier = value; }
        public decimal? RecetteCaisse { get => _recetteCaisse; set => _recetteCaisse = value; }
        public Flux? FluxBorne { get => _fluxBorne; set => _fluxBorne = value; }
        public Flux? FluxCaisse { get => _fluxCaisse; set => _fluxCaisse = value; }
        public int? NbTickets { get => _nbTickets; set => _nbTickets = value; }
        public int? NbAdministrateur { get => _nbAdministrateur; set => _nbAdministrateur = value; }
        public int? PlaceDisponible { get => _placeDisponible; set => _placeDisponible = value; }
        public int? NbAutorite { get => _nbAutorite; set => _nbAutorite = value; }
        public int? NbEgress { get => _nbEgress; set => _nbEgress = value; }
        public int? NbAbonne { get => _nbAbonne; set => _nbAbonne = value; }
        public int? PlaceMax { get => _placeMax; set => _placeMax = value; }
        public int? FluxBorneTotal { get => _fluxBorneTotal; set => _fluxBorneTotal = value; }
        public int? FluxCaisseTotal { get => _fluxCaisseTotal; set => _fluxCaisseTotal = value; }
    }
}
