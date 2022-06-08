namespace RitegeDomain.Database.IRepositories;

using RitegeDomain.Database.Entities.Parking;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface ICaisseRepository : IRepository<Caisse>
{
    public Task<Caisse> GetOneByIdAsync(int caisseId);

    public Task<Caisse> GetOneByNameAsync(string caisseName);
    public Task<IEnumerable<Caisse>> GetAllByIdParkingAsync(int id);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}