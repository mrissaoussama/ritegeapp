namespace RitegeDomain.Database.IRepositories;
public interface IAbonneRepository : IRepository<Abonne>
{
    public Task<IEnumerable<Abonne>> GetAllAsync();
    public Task<Abonne> GetOneByIdAsync(long id);


}