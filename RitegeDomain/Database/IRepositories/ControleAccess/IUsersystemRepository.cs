using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IUsersystemRepository : IRepository<Usersystem>
{

    public Task<Usersystem> GetOneByIdAsync(long id);
    public Task<List<Usersystem>> GetAllAsync();

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}