using System;

public class RabbitMqService
{
    public void CreateChanel
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "my first queue", durable: true, exclusive: false, autoDelete: false,
        arguments: null);
    }
}
