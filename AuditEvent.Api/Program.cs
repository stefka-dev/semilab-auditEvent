using AuditEvent.Storage;
using AuditEvent.Storage.Interfaces;
using AuditEvent.Storage.Models;
using AuditEvent.Storage.Repositories;
using AuditEvent.Worker;
using AuditEvent.Worker.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext, MongoDbContext>();
builder.Services.AddScoped(typeof(IRepository<AuditEventMessage>), typeof(AuditEventRepository));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();