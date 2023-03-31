namespace Tickets.DAL;

public interface IGenericRepo<T> where T : class
{
    List<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    int SaveChanges();
}
