using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace RabbitRepository{

public class Consumer
{
    private ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;
    private EventingBasicConsumer _consumer;

    public void StartConsuming(string queueName, Action<string> onMessageReceived)
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _consumer = new EventingBasicConsumer(_channel);

        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            onMessageReceived(message);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer);
    }
}
}