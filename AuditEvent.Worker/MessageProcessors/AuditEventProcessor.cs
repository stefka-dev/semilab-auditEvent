using System.Text.Json;
using AuditEvent.Storage.Models;
using AuditEvent.Worker.Dtos;
using AuditEvent.Worker.Interfaces;
using AuditEvent.Worker.MessageProcessors.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AuditEvent.Worker.MessageProcessors;

public class AuditEventProcessor : AbstractMessageProcessor<AuditEventProcessor>
{
    private readonly DbContext _dbContext;

    public AuditEventProcessor(ILogger<AuditEventProcessor> logger, DbContext dbContext) : base(logger)
    {
        _dbContext = dbContext;
    }

    protected override async Task TryHandleMessage(ReceivedMessageDto message)
    {
        var entity = new AuditEventMessage()
        {
            Payload = message.Body
        };

        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}