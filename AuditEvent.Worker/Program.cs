using AuditEvent.Storage;
using AuditEvent.Worker;
using AuditEvent.Worker.Interfaces;
using AuditEvent.Worker.QueueAdapters;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<DbContext, MongoDbContext>();
builder.Services.AddScoped<IMessageProcessFactory, MessageProcessFactory>();
builder.Services.AddScoped<IQueueAdapter, RabbitMqAdapter>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();