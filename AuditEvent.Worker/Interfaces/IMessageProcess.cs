using AuditEvent.Worker.Dtos;

namespace AuditEvent.Worker.Interfaces;

public interface IMessageProcess
{
    Task HandleMessage(ReceivedMessageDto message);
}