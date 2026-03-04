using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "my first queue", durable: true, exclusive: false, autoDelete: false,
    arguments: null);

const string message = "Hello World! 02";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "my first queue", body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();