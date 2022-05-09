    using RitegeDomain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RitegeDomain.Model
{
    public class DateAbonnement
    {

        public DateTime dateStart { get; set; }
        public decimal Total { get; set; }
        public List<InfoAbonnementDTO> ListAbonnement { get; set; }
        public DateAbonnement(List<InfoAbonnementDTO> list)
        {
            ListAbonnement = list.Where(x => x.DateActivation == dateStart).ToList();
            Total = ListAbonnement.Sum(x => x.PrixAbonnement);
        }
        public DateAbonnement(List<InfoAbonnementDTO> list, DateTime d)
        {
            dateStart = d;
            ListAbonnement = list.Where(x => x.DateActivation == dateStart).ToList();
            Total = ListAbonnement.Sum(x => x.PrixAbonnement);
        }
        public decimal AbonnementTotal => ListAbonnement.Count;

        public decimal RecetteTotal => ListAbonnement.Sum(x => x.PrixAbonnement);
    }
}
