﻿using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IEventRepository : IRepository<Event>
{

    public Task<Event> GetOneByIdAsync(long id);
    public Task<List<Event>> GetAllByDateAsync(DateTime date);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}