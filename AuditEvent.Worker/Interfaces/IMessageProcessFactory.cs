using AuditEvent.Worker.Dtos;

namespace AuditEvent.Worker.Interfaces;

public interface IMessageProcessFactory
{
    void ProcessMessage(string queueName, ReceivedMessageDto message);
}