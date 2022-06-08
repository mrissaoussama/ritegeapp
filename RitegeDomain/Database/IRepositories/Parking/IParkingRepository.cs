namespace RitegeDomain.Database.IRepositories;

using RitegeDomain.Database.Entities.Parking;
using RitegeDomain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IParkingRepository : IRepository<Parking>
{
    //  public Task<Caisse> GetOneByIdAsync(int ParkingId);
    public Task<Parking> GetOneByIdParkingAsync(int idSociete);
    public Task<Parking> GetOneByNomParkingAsync(string nomParking);
    public Task<IEnumerable<Parking>> GetAllByIdSocieteAsync(int idSociete);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}