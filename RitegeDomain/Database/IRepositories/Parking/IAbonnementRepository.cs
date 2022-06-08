namespace RitegeDomain.Database.IRepositories; using RitegeDomain.Database.Entities.ParkingEntities;

public interface IAbonnementRepository : IRepository<Abonnement>
{
    public Task<IEnumerable<Abonnement>> GetAllAsync();
    public Task<Abonnement> GetOneByIdAsync(long id);


}