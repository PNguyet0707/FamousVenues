using Azure.Messaging.ServiceBus;
using SBShared.Models;
using System.Text;
using System.Text.Json;
const string connectionString = "";
const string topicConnectionString = "";
const string queueName = "personqueue";
const string topicName = "hailietopic";
const string subcriptionName = "Sub02";

#region Queue Receiver 

#region   using QueueClient

//var queueClient = new QueueClient(connectionString, queueName);
//var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
//{
//    MaxConcurrentCalls = 1,
//    AutoComplete = false
//};
//queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
//Console.ReadLine();
//await queueClient.CloseAsync();
//async Task ProcessMessageAsync(Message message, CancellationToken token)
//{
//    var messageJson = Encoding.UTF8.GetString(message.Body);
//    // throw new Exception("Occurred exception when handled receive message");
//    PersonModel person = JsonSerializer.Deserialize<PersonModel>(messageJson);
//    await Task.Delay(TimeSpan.FromMinutes(1));
//    //throw new Exception();
//    Console.WriteLine($"Person received: {person.FirstName} {person.LastName}");
//    await queueClient.CompleteAsync(message.SystemProperties.LockToken);
//}

//async Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
//{
//    Console.WriteLine($"Occured issue when handler receive message: {args.Exception}");
//}
#endregion

#region using Processor 
//// the client that owns the connection and can be used to create senders and receivers
//ServiceBusClient client;

//// the processor that reads and processes messages from the queue
//ServiceBusProcessor processor;

//// The Service Bus client types are safe to cache and use as a singleton for the lifetime
//// of the application, which is best practice when messages are being published or read
//// regularly.
////
//// Set the transport type to AmqpWebSockets so that the ServiceBusClient uses port 443. 
//// If you use the default AmqpTcp, make sure that ports 5671 and 5672 are open.

//// TODO: Replace the <NAMESPACE-CONNECTION-STRING> and <QUEUE-NAME> placeholders
//var clientOptions = new ServiceBusClientOptions()
//{
//    TransportType = ServiceBusTransportType.AmqpWebSockets
//};
//client = new ServiceBusClient(connectionString, clientOptions);

//// create a processor that we can use to process the messages
//// TODO: Replace the <QUEUE-NAME> placeholder
//processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

//try
//{
//    // add handler to process messages
//    processor.ProcessMessageAsync += MessageHandler;

//    // add handler to process any errors
//    processor.ProcessErrorAsync += ErrorHandler;

//    // start processing 
//    await processor.StartProcessingAsync();

//    Console.WriteLine("Wait for a minute and then press any key to end the processing");
//    Console.ReadKey();

//    // stop processing 
//    Console.WriteLine("\nStopping the receiver...");
//    await processor.StopProcessingAsync();
//    Console.WriteLine("Stopped receiving messages");
//}
//finally
//{
//    // Calling DisposeAsync on client types is required to ensure that network
//    // resources and other unmanaged objects are properly cleaned up.
//    await processor.DisposeAsync();
//    await client.DisposeAsync();
//}

// //handle received messages
//async Task MessageHandler(ProcessMessageEventArgs args)
//{
//    //string body = args.Message.Body.ToString();
//    var messageJson = Encoding.UTF8.GetString(args.Message.Body);
//    PersonModel person = JsonSerializer.Deserialize<PersonModel>(messageJson);    
//    // complete the message. message is deleted from the queue. 
//    //Thread.Sleep(80000);
//    //Task.Delay(1000);
//    Console.WriteLine($"Person received: {person.FirstName} {person.LastName}");
//    await args.CompleteMessageAsync(args.Message);
//}

//// handle any errors when receiving messages
//Task ErrorHandler(ProcessErrorEventArgs args)
//{
//    Console.WriteLine($"Occurred issue when handler receive message:{args.Exception.ToString()} ");
//    return Task.CompletedTask;
//}
#endregion


#region Using receiver

//await using var client = new ServiceBusClient(connectionString);
//await using var receiver = client.CreateReceiver(queueName);

//while (true)
//{
//    ServiceBusReceivedMessage? message =
//    await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5));

//    if (message == null)
//    {
//        Thread.Sleep(10000);
//        continue;
//    }

//    try
//    {
//        Console.WriteLine("Start handling message");
//        var body = message.Body.ToString();
//        var person = JsonSerializer.Deserialize<PersonModel>(body);
//        throw new Exception();
//    TODO: Deserialize & business logic
//    var data = JsonSerializer.Deserialize<YourModel>(body);

//        await Task.Delay(TimeSpan.FromMinutes(2));
//        Console.WriteLine($"Received: {person.FirstName} {person.LastName}");
//        await receiver.CompleteMessageAsync(message);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//        await receiver.AbandonMessageAsync(message);
//    }
//}


//var serviceBusClient = new ServiceBusClient(connectionString);
//var receiver = serviceBusClient.CreateReceiver(queueName);
//while (true)
//{
//    try
//    {
//        ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(4));
//        if (message is null)
//        {
//            continue;
//        }
//        var person = JsonSerializer.Deserialize<PersonModel>(message.Body);
//        await Task.Delay(TimeSpan.FromMinutes(3));
//        await receiver.CompleteMessageAsync(message);
//        Console.WriteLine($"Received message info: {person.FirstName} {person.LastName}");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Occurred exception when handling message: {ex.ToString()}");
//        continue;
//    }
//}
// Replaced the per-iteration client/receiver creation with long-lived instances and added null/error handling.
//await using var serviceBusClient = new ServiceBusClient(connectionString);
//await using var receiver = serviceBusClient.CreateReceiver(queueName);

//while (true)
//{
//    ServiceBusReceivedMessage? message = await receiver.ReceiveMessageAsync();
//    if (message == null)
//    {
//        Console.WriteLine("No message received in 5s.");
//        continue;
//    }

//    var body = message.Body.ToString();
//    try
//    {
//        var person = JsonSerializer.Deserialize<PersonModel>(body);
//        if (person is null)
//        {
//            Console.WriteLine("Deserialized person is null - dead lettering message.");
//            await receiver.DeadLetterMessageAsync(message, "DeserializationFailed", "PersonModel deserialized to null");
//            continue;
//        }

//        Console.WriteLine($"Received message info: {person.FirstName} {person.LastName}");
//        await receiver.CompleteMessageAsync(message);
//    }
//    catch (JsonException jex)
//    {
//        Console.WriteLine($"JSON deserialization error: {jex.Message} - dead lettering message.");
//        await receiver.DeadLetterMessageAsync(message, "JsonError", jex.Message);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Processing failed: {ex.Message} - abandoning message.");
//        await receiver.AbandonMessageAsync(message);
//    }
//}



#endregion



#endregion


#region    Subcription Receiver 

// the client that owns the connection and can be used to create senders and receivers
ServiceBusClient client;

// the processor that reads and processes messages from the subscription
ServiceBusProcessor processor;

// The Service Bus client types are safe to cache and use as a singleton for the lifetime
// of the application, which is best practice when messages are being published or read
// regularly.
//
// Create the clients that we'll use for sending and processing messages.
// TODO: Replace the <NAMESPACE-CONNECTION-STRING> placeholder
client = new ServiceBusClient(topicConnectionString);

// create a processor that we can use to process the messages
// TODO: Replace the <TOPIC-NAME> and <SUBSCRIPTION-NAME> placeholders
processor = client.CreateProcessor(topicName,subcriptionName, new ServiceBusProcessorOptions());

try
{
    // add handler to process messages
    processor.ProcessMessageAsync += MessageHandler;

    // add handler to process any errors
    processor.ProcessErrorAsync += ErrorHandler;

    // start processing 
    await processor.StartProcessingAsync();

    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();

    // stop processing 
    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    // Calling DisposeAsync on client types is required to ensure that network
    // resources and other unmanaged objects are properly cleaned up.
    await processor.DisposeAsync();
    await client.DisposeAsync();
}
// handle received messages
async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = Encoding.UTF8.GetString(args.Message.Body);
    PersonModel person = JsonSerializer.Deserialize<PersonModel>(body);
    Console.WriteLine($"Received person infor: {person.FirstName} {person.LastName}");

    // complete the message. messages is deleted from the subscription. 
    await args.CompleteMessageAsync(args.Message);
}

// handle any errors when receiving messages
Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}
#endregion