using AuditEvent.Worker.Dtos;

namespace AuditEvent.Worker.Interfaces;

public interface IQueueAdapter
{
    event Action<string, ReceivedMessageDto> OnReceiveMessage;
    Task Subscribe(string queueName);
}