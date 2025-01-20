namespace AuditEvent.Worker.Interfaces;

public interface IMessageParse<T> where T : class
{
    T Parse(byte[] message);
}