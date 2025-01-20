namespace AuditEvent.Worker.Dtos.MessageProcessorDtos;

public record AuditEventDto 
{
    public string EventName { get; set; }
    public string Status { get; set; }
}