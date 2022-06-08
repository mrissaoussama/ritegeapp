namespace RitegeDomain.Database.IRepositories; using RitegeDomain.Database.Entities.ParkingEntities;

public interface IAffectationabonnementRepository : IRepository<Affectationabonnement>
{
    public Task<IEnumerable<Affectationabonnement>> GetAllAsync();
    public Task<IEnumerable<Affectationabonnement>> GetAllByAbonneIdAsync(long id);
    public Task<IEnumerable<Affectationabonnement>> GetAllByAbonnementIdAsync(long id);

    public Task<IEnumerable<Affectationabonnement>> GetAllByIdWithDatesAsync(long? id, DateTime start, DateTime finish);
    public Task<IEnumerable<Affectationabonnement>> GetAllByNameAndDatesAsync(string name, DateTime start, DateTime finish);

    public Task<Affectationabonnement> GetOneByIdAsync(long id);



}