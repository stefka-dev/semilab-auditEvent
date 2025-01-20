using AuditEvent.Worker.Dtos;
using AuditEvent.Worker.Interfaces;

namespace AuditEvent.Worker.MessageProcessors.Abstracts;

public abstract class AbstractMessageProcessor<P> : IMessageProcess
    where P : IMessageProcess
{
    private readonly ILogger<P> _logger;

    protected AbstractMessageProcessor(ILogger<P> logger)
    {
        _logger = logger;
    }

    public async Task HandleMessage(ReceivedMessageDto message)
    {
        try
        {
            _logger.LogInformation($"Started message handling: {message.Header.MessageId}");
            await TryHandleMessage(message);
            _logger.LogInformation($"Finished message handling: {message.Header.MessageId}");
        }
        catch (Exception e)
        {
            _logger.LogError($"Can't handle message! {message} exp {e}");
        }
    }

    protected abstract Task TryHandleMessage(ReceivedMessageDto message);
}