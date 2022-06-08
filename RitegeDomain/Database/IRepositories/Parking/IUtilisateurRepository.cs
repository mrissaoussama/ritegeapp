using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.IRepositories;
public interface IUtilisateurRepository : IRepository<Utilisateur>
{

    public Task<Utilisateur> GetOneByIdAsync(long id);
    public Task<Utilisateur> GetOneByLoginAndMotDePasseAsync(string login, string password);
    public Task<string?> Login(string login, string password);
    public Task<Utilisateur> GetOneByNumAccessCardAsync(string number);
    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}