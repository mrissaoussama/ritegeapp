namespace RitegeDomain.Database.IRepositories; using RitegeDomain.Database.Entities.ParkingEntities;

public interface IAbonneRepository : IRepository<Abonne>
{
    public Task<IEnumerable<Abonne>> GetAllAsync();
    public Task<Abonne> GetOneByIdAsync(long id);


}