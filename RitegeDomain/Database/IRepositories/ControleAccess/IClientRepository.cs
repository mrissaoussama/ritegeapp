using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IClientRepository : IRepository<Client>
{

    public Task<Client> GetOneByIdAsync(int id);
    //public Task<List<Client>> GetAllAsync();
   public Task<Client> GetOneByEmailAndPasswordAsync(string email,string password);

 //   public Task<Client> GetAllByIdSociete(string email, string password);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}