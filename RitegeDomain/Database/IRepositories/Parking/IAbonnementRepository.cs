namespace RitegeDomain.Database.IRepositories;
public interface IAbonnementRepository : IRepository<Abonnement>
{
    public Task<IEnumerable<Abonnement>> GetAllAsync();
    public Task<Abonnement> GetOneByIdAsync(long id);


}