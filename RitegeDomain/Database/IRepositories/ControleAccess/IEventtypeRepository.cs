using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IEventtypeRepository : IRepository<Eventtype>
{

    public Task<Eventtype> GetOneByIdAsync(long id);
    public Task<List<Eventtype>> GetAllAsync();

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}