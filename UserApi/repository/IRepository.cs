namespace UserApi.repository;

public interface IRepository<T, TId>
{
    T Add(T entity);
    T Delete(TId id);
    T Get(TId id);
    T Update(T entity);
}