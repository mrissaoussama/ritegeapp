using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IDoorRepository : IRepository<Door>
{

    public Task<Door> GetOneByIdSocieteAsync(long id);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}