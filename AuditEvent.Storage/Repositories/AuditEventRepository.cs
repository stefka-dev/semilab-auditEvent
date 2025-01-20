using AuditEvent.Storage.Interfaces;
using AuditEvent.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditEvent.Storage.Repositories;

public class AuditEventRepository : IRepository<AuditEventMessage>
{
    private readonly DbContext _dbContext;

    public AuditEventRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<AuditEventMessage> GetAll()
    {
        return _dbContext.Set<AuditEventMessage>();
    }
    public bool IntegrityCheck()
    {
        var items = _dbContext.Set<AuditEventMessage>().AsEnumerable();
        foreach (var item in items)
        {
            if (!HashHelper.VerifyHash(item)) return false;
        }

        return true;
    }

    public async Task Add(AuditEventMessage entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}