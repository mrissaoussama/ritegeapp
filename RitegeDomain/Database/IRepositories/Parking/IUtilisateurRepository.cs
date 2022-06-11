using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.IRepositories;
public interface IUtilisateurRepository : IRepository<Utilisateur>
{

    public Task<Utilisateur> GetOneByIdAsync(long id);
    public Task<IEnumerable<Utilisateur>> GetAllByIdParkingAsync(int id);
    public Task<IEnumerable<Utilisateur>> GetAllByIdSocieteAsync(int id);
    public Task<Utilisateur> GetOneByLoginAsync(string login);

    public Task<Utilisateur> GetOneByLoginAndMotDePasseAsync(string login, string password);
    public Task<Utilisateur> GetOneByNumAccessCardAsync(string number);
    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}