using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService;
using System.Text;
using System.Text.Json;

const string exchangeName = "HailieExchange";

#region Receive message in queue and workqueue 

//IRabbitService rabbitServce = new RabbitService();
//var channel = await rabbitServce.CreateChannel();
//await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
//Console.WriteLine("Waiting for messages.");

//var consumer = new AsyncEventingBasicConsumer(channel);
//consumer.ReceivedAsync += async (model, ea) =>
//{
//    try
//    {
//        var body = ea.Body.ToArray();
//        var message = Encoding.UTF8.GetString(body);
//        var customer = JsonSerializer.Deserialize<Customer>(message);

//        Console.WriteLine($"Received message about customer: {customer?.FullName} - {customer?.Email}");

//        await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//        await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
//    }
//};
//await channel.BasicConsumeAsync(queue: channel.CurrentQueue, autoAck: false, consumer: consumer);
//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();

#endregion



#region  receive message for fannout exchange

//IRabbitService rabbitServce = new RabbitService();
//var channel = await rabbitServce.CreateExchangeChannelByType(RabbitExchangeType.Fanout);
//await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
//var queueName = await channel.QueueDeclareAsync();

//await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: string.Empty);

//Console.WriteLine("Waiting for messages.");

//var consumer = new AsyncEventingBasicConsumer(channel);
//consumer.ReceivedAsync += async (model, ea) =>
//{
//    try
//    {
//        var body = ea.Body.ToArray();
//        var message = Encoding.UTF8.GetString(body);
//        var customer = JsonSerializer.Deserialize<Customer>(message);

//        Console.WriteLine($"Received message about customer: {customer?.FullName} - {customer?.Email}");

//        await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//        await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
//    }
//};
//await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();
#endregion

#region

IRabbitService rabbitServce = new RabbitService();
var channel = await rabbitServce.CreateExchangeChannelByType(RabbitExchangeType.Direct);
await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
var queueName = await channel.QueueDeclareAsync();

await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: string.Empty);

Console.WriteLine("Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    try
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var customer = JsonSerializer.Deserialize<Customer>(message);

        Console.WriteLine($"Received message about customer: {customer?.FullName} - {customer?.Email}");

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
#endregion