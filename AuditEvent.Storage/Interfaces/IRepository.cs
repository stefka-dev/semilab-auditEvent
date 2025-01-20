namespace AuditEvent.Storage.Interfaces;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    bool IntegrityCheck();
    IEnumerable<T> GetAll();
}