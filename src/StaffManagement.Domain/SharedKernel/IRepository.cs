namespace StaffManagement.Domain.SharedKernel;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> Get();
    Task<TEntity> Get(Guid id);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}