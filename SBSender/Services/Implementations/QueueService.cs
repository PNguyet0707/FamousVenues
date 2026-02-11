using Azure.Messaging.ServiceBus;
using SBSender.Services.Interfaces;
using System.Text;
using System.Text.Json;

public class QueueService : IQueueService
{
    private readonly ServiceBusClient _serviceBusClient;
    private const string queueName = "personqueue";
    private const string topicName = "hailietopic";

    public QueueService(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

   
    public async Task SendMessageAsync<T>(T serviceBusMessage)
    {
        var sender = _serviceBusClient.CreateSender(queueName);
        string messageBody = JsonSerializer.Serialize(serviceBusMessage);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
        var scheduleTime = DateTimeOffset.UtcNow.AddMinutes(2);
        await sender.ScheduleMessageAsync(message, scheduleTime);
    }
    public async Task SendBatchMessageAsync<T>(IList<T> serviceBusMessages)
    {
        await using var sender = _serviceBusClient.CreateSender(queueName);

        var batch = await sender.CreateMessageBatchAsync();

        foreach (var item in serviceBusMessages)
        {
            var body = JsonSerializer.Serialize(item);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(body));

            if (!batch.TryAddMessage(message))
            {
                await sender.SendMessagesAsync(batch);
                batch = await sender.CreateMessageBatchAsync();

                if (!batch.TryAddMessage(message))
                {
                    throw new InvalidOperationException(
                        "Message is too large to fit in a batch.");
                }
            }
        }

        if (batch.Count > 0)
        {
            await sender.SendMessagesAsync(batch);
        }
    }

    public async Task SendMessageToTopicAsync<T>(T serviceBusMessage)
    {

        var topicSender = _serviceBusClient.CreateSender(topicName);
        var messageBody = JsonSerializer.Serialize(serviceBusMessage);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
        await topicSender.SendMessageAsync(message);
    }
}
