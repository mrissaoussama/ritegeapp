using RitegeDomain.DTO;

namespace RitegeDomain.Database.IRepositories;
public interface IInfoAbonnementDTORepository : IRepository<InfoAbonnementDTO>
{

    public Task<IEnumerable<InfoAbonnementDTO?>> GetAllByNameAndDatesAsync(string? name, DateTime start, DateTime finish);




}