using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using AuditEvent.Storage.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuditEvent.Storage.Models;

public class AuditEventMessage : ITamperProofEntity
{
    [BsonId] public ObjectId Id { get; set; }
    public string Payload { get; set; }
    public string Hash { get; set; }
    public DateTime CreatedAt { get; set; }
}