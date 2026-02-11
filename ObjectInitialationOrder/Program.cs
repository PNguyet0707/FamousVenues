using ObjectInitializationOrder;

Console.WriteLine("Enter queue name");
string queue1 = Console.ReadLine();
var client1 = new Initialization(queue1);
Console.WriteLine($"Information about client 1: cStr {Initialization.ConnectStr}, {Initialization.ConnectCount}, {client1.QueueName}");

Console.WriteLine("Enter queue name");
string queue2 = Console.ReadLine();
var client2 = new Initialization(queue1);
Console.WriteLine($"Information about client 2: cStr {Initialization.ConnectStr}, {Initialization.ConnectCount}, {client2.QueueName}");

Console.ReadLine();