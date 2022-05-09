global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
using RitegeDomain.Database.Entities.Parking;

namespace RitegeDomain.Database.IRepositories;
public interface ITicketRepository : IRepository<Ticket>
{

    public Task<IEnumerable<Ticket>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end);
    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}