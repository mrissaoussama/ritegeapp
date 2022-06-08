using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface ISocieteRepository : IRepository<Societe>
{

    public Task<Societe> GetOneByIdAsync(int id);
    //public Task<List<Client>> GetAllAsync();
   public Task<Societe> GetOneByNameAsync(string name);

 //   public Task<Client> GetAllByIdSociete(string email, string password);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}