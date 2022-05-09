

namespace RitegeDomain.Database.Repositories;
public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
{
    public GenericRepository() { }
}