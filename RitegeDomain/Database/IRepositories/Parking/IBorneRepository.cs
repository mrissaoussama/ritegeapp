namespace RitegeDomain.Database.IRepositories;

using RitegeDomain.Database.Entities.Parking;
using System;
using System.Collections.Generic;
using RitegeDomain.Database.Entities.ParkingEntities;

using System.Threading.Tasks;
public interface IBorneRepository : IRepository<Borne>
{

    public Task<Borne> GetOneByNameAsync(string borneName);
    public Task<Borne> GetOneByIdAsync(int borneId);
    public Task<IEnumerable<Borne>> GetAllByIdParkingAsync(int id);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}