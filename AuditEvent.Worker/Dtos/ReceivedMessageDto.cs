using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;

namespace AuditEvent.Worker.Dtos;

public record ReceivedMessageDto
{
    public ReceivedMessageHeaderDto Header { get; set; }
    public string Body { get; set; }
}

public record ReceivedMessageHeaderDto
{
    public string? MessageId { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Location { get; set; }

    public ReceivedMessageHeaderDto(AmqpTimestamp publishDate, object? location, string? messageId)
    {
        MessageId = messageId;
        PublishDate = new DateTime(publishDate.UnixTime);
        Location = location is string convertedLocation ? convertedLocation : null;
    }
}