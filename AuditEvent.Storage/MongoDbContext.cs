using AuditEvent.Storage.Interceptors;
using AuditEvent.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace AuditEvent.Storage;

public class MongoDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<AuditEventMessage> AuditEventMessages { get; set; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetRequiredSection("Database:Connection")?.Value ??
                                  throw new Exception("Connection string not found");
        string databaseName = _configuration.GetRequiredSection("Database:DatabaseName")?.Value ??
                              throw new Exception("DatabaseName string not found");

        optionsBuilder.UseMongoDB(connectionString, databaseName);
        optionsBuilder.AddInterceptors(new HashInterceptor());
    }
}