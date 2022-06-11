namespace RitegeDomain.Database.IRepositories;

using RitegeDomain.Database.Entities.Parking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RitegeDomain.Database.Entities.ParkingEntities;

public interface ISessionRepository : IRepository<Session>
{

    public Task<IEnumerable<Session>> GetAllByIdAndDateAsync(long? id, DateTime dateStart, DateTime dateEnd);
    public Task<IEnumerable<Session>> GetAllByCaisseAndDateAsync(long? id, DateTime dateStart, DateTime dateEnd);
    public Task<Session> GetCurrentSessionAsync(int idParking,int idCaisse);
    public Task<Session> GetOneByTicketLogCaissierAndTicketDatesAsync(string logCaissier, DateTime dateStart, DateTime? dateEnd);
    public Task<int> UpdateSessionEarningsByIdAsync(int Idsessions,decimal? pricetoAdd);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}