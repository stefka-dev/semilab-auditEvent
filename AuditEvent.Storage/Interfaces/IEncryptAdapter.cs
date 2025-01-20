namespace AuditEvent.Storage.Interfaces;

public interface IEncryptAdapter
{
    string Encrypt<T>(T item);
    T? Decrypt<T>(string encrypted);
}