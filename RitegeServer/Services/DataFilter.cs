    using RitegeDomain.DTO;
using System.Diagnostics;
namespace RitegeServer.Services
{
    public class DataFilter
    {
        public DataFilter()
        {

        }

        public IEnumerable<InfoAbonnementDTO> FilterAbonnementDTO(IEnumerable<InfoAbonnementDTO> listToFilter,
            DateTime dateStart, DateTime dateEnd, string? abonneName)
        {
            var result = listToFilter.Where(dto => dto.DateActivation.Date >= dateStart.Date && dto.DateFinActivation.Date <= dateEnd.Date).ToList();

            if (string.IsNullOrEmpty(abonneName) == false)
            {

                result = result.Where(p => p.NomPrenomAbonne != null && p.NomPrenomAbonne.ToLower().Contains(abonneName.ToLower())).ToList();
            }
            return result;

        }
        public IEnumerable<InfoSessionsDTO> FilterSessionDTO(IEnumerable<InfoSessionsDTO> listToFilter,
       DateTime dateStart, DateTime dateEnd, string? caissierName)
        {
            var result = listToFilter.Where(dto => dto.DateStartSession.Date >= dateStart.Date && dto.DateEndSession.Date <= dateEnd.Date).ToList();

            if (string.IsNullOrEmpty(caissierName) == false)
            {

                result = result.Where(p => p.Caissier != null && p.Caissier.ToLower().Contains(caissierName.ToLower())).ToList();
            }
            return result;

        }
    
    }
}
