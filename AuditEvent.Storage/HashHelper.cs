using System.Security.Cryptography;
using System.Text;
using AuditEvent.Storage.Interfaces;

namespace AuditEvent.Storage;

public static class HashHelper
{
    private static string hashInputGenerator(ITamperProofEntity entity)
    {
        return $"{entity.Payload}|{entity.CreatedAt}";
    }
    public static string ComputeHash(ITamperProofEntity entity)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(hashInputGenerator(entity));
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static bool VerifyHash(ITamperProofEntity entity)
    {
        return ComputeHash(entity) == entity.Hash;
    }
}