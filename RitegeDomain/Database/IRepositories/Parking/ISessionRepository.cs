namespace RitegeDomain.Database.IRepositories;

using RitegeDomain.Database.Entities.Parking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface ISessionRepository : IRepository<Session>
{

    public Task<IEnumerable<Session>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end);
    public Task<IEnumerable<Session>> GetAllByCaisseAndDateAsync(long? id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}