    using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Linq;

namespace RitegeDomain.Model
{
    public class GroupAbonnement
    {
        public string NomPrenomAbonne { get; set; }
        public List<InfoAbonnementDTO> ListAbonnement { get; set; }
        public GroupAbonnement(List<InfoAbonnementDTO> list)
        {
            ListAbonnement = list.Where(x => x.NomPrenomAbonne == NomPrenomAbonne).ToList();
        }
        public GroupAbonnement(List<InfoAbonnementDTO> list, string nom)
        {
            NomPrenomAbonne = nom;
            ListAbonnement = list.Where(x => x.NomPrenomAbonne == NomPrenomAbonne).ToList();
        }
        public int AbonnementCount => ListAbonnement.Count;

        public decimal AbonnementTotal => ListAbonnement.Sum(x => x.PrixAbonnement);
    }
}
