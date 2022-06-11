using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.IRepositories;
public interface IEventDTORepository : IRepository<EventDTO>
{

    public Task<List<EventDTO>> GetAllByIdSocieteAndDateAsync(int idSociete, DateTime dateStart,DateTime dateEnd);
    public Task<List<EventDTO>> GetAlertsByIdSocieteAndDateAsync(int idSociete, DateTime dateStart, DateTime dateEnd);

    // public Task<IEnumerable<Ticket>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end);
    // public Task<IEnumerable<Session>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, DateTime start, DateTime end);
    //  public Task<Affectationabonnement> GetOneByIdAsync(long id);



}