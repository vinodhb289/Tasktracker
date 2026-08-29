using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using TaskService.Models;

namespace TaskService.Events;
public class TaskEventPublisher :IAsyncDisposable
{
    
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public TaskEventPublisher(IConfiguration config)
    {
        var factory = new ConnectionFactory { HostName = config["RabbitMQ:Host"]?? "localhost"};
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }

    public async Task PublishAsync(string eventType, TaskItem task)
    {
        await _channel.QueueDeclareAsync(queue: "task-events", durable: true, exclusive: false, autoDelete: false);

        var payload = JsonSerializer.Serialize(new { EventType = eventType, Task = task });
        var body = Encoding.UTF8.GetBytes(payload);

        await _channel.BasicPublishAsync(exchange: "", routingKey: "task-events", body: body);
    }
    public async ValueTask DisposeAsync()
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();
    }
}