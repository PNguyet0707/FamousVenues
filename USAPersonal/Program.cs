
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService;
using System.Text;
using System.Text.Json;

Console.Title = "ToppicUSAConsumer";
const string exchangeName = "HailieExchange";
const string bindingKey = "*.Personal.*";
RabbitService rabbitServce = new RabbitService();
var channel = await rabbitServce.CreateExchangeChannelByType(RabbitExchangeType.Topic);
await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
var queueName = await channel.QueueDeclareAsync();
await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: bindingKey);

Console.WriteLine("Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    try
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var customer = JsonSerializer.Deserialize<Customer>(message);

        Console.WriteLine($"Received message about customer: {customer?.FullName} - {customer?.CustomerType.ToString()} - {customer?.Country} - {customer?.YearOfBirth}");

        await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
    }
};
await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();