using RabbitMQ.Client;

namespace Common.Rabbitmq;

public class CommonRabbitmq
{
    public Task<IConnection> CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            VirtualHost = "RabbitMqDemo"
        };

        return factory.CreateConnectionAsync();
    }
}
