using RitegeDomain.DTO;

namespace RitegeDomain.Database.IRepositories;
public interface IInfoSessionsDTORepository : IRepository<InfoSessionsDTO>
{

    public Task<IEnumerable<InfoSessionsDTO?>> GetAllByNameAndDatesAsync(string? name, DateTime start, DateTime finish);




}