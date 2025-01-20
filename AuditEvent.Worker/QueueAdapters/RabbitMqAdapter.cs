using System.Text;
using AuditEvent.Worker.Dtos;
using AuditEvent.Worker.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AuditEvent.Worker.QueueAdapters;

public class RabbitMqAdapter : IQueueAdapter
{
    public event Action<string, ReceivedMessageDto>? OnReceiveMessage;

    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IChannel _channel;
    private readonly ILogger<RabbitMqAdapter> _logger;

    public RabbitMqAdapter(ILogger<RabbitMqAdapter> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = _configuration.GetRequiredSection("Queue:HostName").Get<string>() ??
                       throw new Exception("Queue:HostName is missing")
        };

        _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }


    public async Task Subscribe(string queueName)
    {
        await _channel.QueueDeclareAsync(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _logger.LogInformation($"Subscribed to queue: {queueName}");

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            ReceivedMessageDto message = new()
            {
                Header = new(ea.BasicProperties.Timestamp, ea.BasicProperties?.Headers?["SourceLocation"],
                    ea.BasicProperties?.MessageId),
                Body = Encoding.UTF8.GetString(ea.Body.ToArray())
            };
            
            _logger.LogInformation($"[x] Received: {message}");
            OnReceiveMessage?.Invoke(queueName, message);
        };

        await _channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);
    }
}