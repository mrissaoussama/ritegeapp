using RitegeDomain.Database.Entities.ControleAccess;

namespace RitegeDomain.Database.IRepositories;
public interface IDoorRepository : IRepository<Door>
{
    public Task<Door> GetOneByIdAsync(long id);
    public Task<int> UpdateDoorStateByIdAsync(long id,bool state);

    public Task<Door> GetOneByIdSocieteAsync(long id);
    public Task<IEnumerable<Door>> GetAllByIdParkingAsync(int idparking);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}