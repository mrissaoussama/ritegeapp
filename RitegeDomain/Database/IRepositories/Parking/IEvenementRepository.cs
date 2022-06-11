using RitegeDomain.Database.Entities.ParkingEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RitegeDomain.Database.IRepositories
{
    public interface IEvenementRepository : IRepository<Evenement>
    {
        public Task<int> Add(DateTime dateEvent, string descriptionEvent,string typeEvent,int? idCaisse,int?idBorne,string logCaissier);
        public Task<int> GetTodayAdministrateurAsync(int idParking, int idCaisse);
        public Task<int> GetTodayAuthoriteAsync(int idParking, int idCaisse);
        public Task<int> GetTodayAbonneAsync(int idParking, int idCaisse);

        public Task<IEnumerable<Evenement>> GetLast10Async(bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetLast10ByBorneAsync(long id, bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetLast10ByCaisseAndBorneAsync(long idCaisse,long idBorne, bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetLast10ByCaisseAsync(long id, bool AlertsOnly);

        public Task<IEnumerable<Evenement>> GetAllByBorneAndDateAsync(long id, DateTime date, bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetAllByDateAsync(DateTime date, bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetAllByCaisseAndDateAsync(long id, DateTime date, bool AlertsOnly);
        public Task<IEnumerable<Evenement>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, long idBorne, DateTime date, bool AlertsOnly);

        //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



    }
}