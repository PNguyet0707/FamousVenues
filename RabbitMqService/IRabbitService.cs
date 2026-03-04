using RabbitMQ.Client;

namespace RabbitMqService
{
    public interface IRabbitService
    {
        Task<IChannel> CreateChannel();
        Task SendMessage(byte[] message);
        Task SendMessageToExchange(byte[] message, RabbitExchangeType exchangeType, string bindingKey = "");
        Task<IChannel> CreateExchangeChannelByType(RabbitExchangeType exchangeType);
    }
}