using System.Text.Json.Nodes;

namespace AuditEvent.Storage.Interfaces;

public interface ITamperProofEntity
{
    string Payload { get; set; }
    string Hash { get; set; }
    DateTime CreatedAt { get; set; }
}