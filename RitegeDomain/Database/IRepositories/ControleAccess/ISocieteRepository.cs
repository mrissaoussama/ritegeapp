using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface ISocieteRepository : IRepository<Societe>
{

    public Task<Societe> GetOneByIdAsync(int id);
    public Task<Societe> GetOneByIdParkingAsync(int idparking);
    public Task<decimal> GetCompanyEarningsByIdAsync(int idSociete);
    public Task<Societe> GetOneByNameAsync(string name);
    public Task<Societe> GetOneByIdDoorAsync(int idDoor);

 //   public Task<Client> GetAllByIdSociete(string email, string password);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}