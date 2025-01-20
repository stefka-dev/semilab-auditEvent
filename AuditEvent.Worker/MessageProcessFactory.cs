using AuditEvent.Worker.Dtos;
using AuditEvent.Worker.Interfaces;
using AuditEvent.Worker.MessageProcessors;
using Microsoft.EntityFrameworkCore;

namespace AuditEvent.Worker;

public class MessageProcessFactory : IMessageProcessFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DbContext _dbContext;
    private readonly ILogger<MessageProcessFactory> _logger;
    public MessageProcessFactory(ILoggerFactory loggerFactory, DbContext dbContext, ILogger<MessageProcessFactory> logger)
    {
        _loggerFactory = loggerFactory;
        _dbContext = dbContext;
    }

    public void ProcessMessage(string queueName, ReceivedMessageDto message)
    {
        switch (queueName.ToLower())
        {
            case "auditevent":
                (new AuditEventProcessor(CreateLogger<AuditEventProcessor>(),_dbContext)).HandleMessage(message).GetAwaiter().GetResult();
                break;
            default:
                _logger.LogError($"Invalid queue name: {queueName}");
                throw new NotImplementedException("Queue processor is not implemented!");
        };
    }

    private static ILogger<T> CreateLogger<T>() where T : IMessageProcess => new LoggerFactory().CreateLogger<T>();
}