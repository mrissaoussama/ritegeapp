using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IControllerRepository : IRepository<Controller>
{

    public Task<Controller> GetOneByIdAsync(long id);
    public Task<List<Controller>> GetAllAsync();

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}