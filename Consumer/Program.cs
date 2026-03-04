using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService;
using System.Text;
using System.Text.Json;

const string queueName = "customer-queue";
var channel = await RabbitService.CreateChannel(queueName);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var customer = JsonSerializer.Deserialize<Customer>(message);
    Console.WriteLine($"Received message about customer: {customer.FullName} - {customer.Email}.");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync("my first queue", autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();