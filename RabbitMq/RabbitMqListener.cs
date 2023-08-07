
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitRepository;


namespace RabbitRepository{

using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
public class RabbitMqConsumer
{
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqConsumer(string queueName, string hostName)
    {
        _queueName = queueName;
        var factory = new ConnectionFactory
        {
            HostName = hostName,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    private void OnMessageReceived(object model, BasicDeliverEventArgs args)
    {
        var body = args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);    
        Console.WriteLine($"Received message: {message}");
    }

    public void StopListening()
    {
       
        _channel.Close();
        _connection.Close();
    }
}


}

/*

	public class RabbitMqListener : IRabbitMqListener
{
	string _lastmessage;

public void Listen()
    {
 	  
		//string[] args = array.Split(' ');
		var factory = new ConnectionFactory { HostName = "localhost" };

		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);
// declare a server-named queue
		var queueName = "MyQueue";

		

		var consumer = new EventingBasicConsumer(channel);
		consumer.Received += (model, ea) =>
		{
    	var body = ea.Body.ToArray();
    	var message = Encoding.UTF8.GetString(body);
    	var routingKey = ea.RoutingKey;
    	Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
		_lastmessage = message;
		};
		channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
    	}

	public string GetLastMessage(){
			Listen();
			return _lastmessage;
		}
		}
*/



/*
 public void Main(string[] args)
    {
 	  
		//string[] args = array.Split(' ');
		var factory = new ConnectionFactory { HostName = "localhost" };

		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);
// declare a server-named queue
		var queueName = channel.QueueDeclare().QueueName;

		if (args.Length < 1)
		{
    	Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                            Environment.GetCommandLineArgs()[0]);
    	Console.WriteLine(" Press [enter] to exit.");
    	Console.ReadLine();
    	Environment.ExitCode = 1;
    	return;
		}

		foreach (var severity in args)
		{
    	channel.QueueBind(queue: queueName,
                      exchange: "direct_logs",
                      routingKey: severity);
		}
		_lastmessage = args;
		Console.WriteLine(" [*] Waiting for messages.");

		var consumer = new EventingBasicConsumer(channel);
		consumer.Received += (model, ea) =>
		{
    	var body = ea.Body.ToArray();
    	var message = Encoding.UTF8.GetString(body);
    	var routingKey = ea.RoutingKey;
    	Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
		};
		channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
    	}
*/
	


























/*
namespace RabbitRepository{
public class RabbitMqListener : BackgroundService, IRabbitMqListener
{
	private IConnection _connection;
	private IModel _channel;
	public RabbitMqListener()
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();
		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
			// Каким-то образом обрабатываем полученное сообщение
			
			Debug.WriteLine($"Получено сообщение: {content}");
            //отправить что-то на сервак 
			object[] myData = {content};
			  InvokeController("RabbitMq", "GetMessage", myData);
			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("MyQueue", false, consumer);

		return Task.CompletedTask;
	}
	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();
		base.Dispose();
	}
	public void InvokeController(string controllerName, string methodName, object[] parameters)
{
	 var controllerType = Type.GetType($"{controllerName}Controller");
    var controllerInstance = Activator.CreateInstance(controllerType);
    var method = controllerType.GetMethod(methodName);

    if (method != null)
    {
        method.Invoke(controllerInstance, parameters);
    }
    else
    {
        Console.WriteLine("Метод не найден");
    }
}

}
}

*/