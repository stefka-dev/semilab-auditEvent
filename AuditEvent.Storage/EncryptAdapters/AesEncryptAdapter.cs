using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AuditEvent.Storage.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuditEvent.Storage.EncryptAdapters;

public class AesEncryptAdapter : IEncryptAdapter
{
    private readonly IConfiguration _configuration;
    private readonly string _encryptionKey;

    public AesEncryptAdapter(IConfiguration configuration)
    {
        _configuration = configuration;
        _encryptionKey = _configuration["AES:EncryptionKey"] ?? "DEFAULT SECRET KEY";
    }

    public string Encrypt<T>(T item)
    {
        var json = JsonSerializer.Serialize(item);


        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(json);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    public T? Decrypt<T>(string encrypted)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] cipherBytes = Convert.FromBase64String(encrypted);
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(plainBytes));
            }
        }
    }
}