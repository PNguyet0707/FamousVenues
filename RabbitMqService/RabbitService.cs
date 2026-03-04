using RabbitMQ.Client;

namespace RabbitMqService
{
    public class RabbitService : IDisposable, IRabbitService
    {
        private readonly ConnectionFactory _factory;
        private readonly string exchangeName = "HailieExchange";
        private readonly string queueName = "customer-queue-persistent";
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1); 
        private IConnection _connection;
        private IChannel _channel;
        const ushort MAX_OUTSTANDING_CONFIRMS = 256;
        private CreateChannelOptions channelOpts = new CreateChannelOptions(
                                                        publisherConfirmationsEnabled: true,
                                                        publisherConfirmationTrackingEnabled: true,
                                                        outstandingPublisherConfirmationsRateLimiter: new ThrottlingRateLimiter(MAX_OUTSTANDING_CONFIRMS));
        public RabbitService()
        {
            _factory = new ConnectionFactory { 
                HostName = "localhost"
            };
        }
        public async Task<IChannel> CreateChannel()
        {
            try
            {
                await _connectionLock.WaitAsync();
                if (_connection == null || !_connection.IsOpen)
                    _connection = await _factory.CreateConnectionAsync();

                if (_channel == null || !_channel.IsOpen)
                {
                    _channel = await _connection.CreateChannelAsync(channelOpts);
                    await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    //await _channel.ConfirmSelect();
                }
                return _channel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erorr orcured when create channel:{ex.ToString()}");
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }
        public async Task<IChannel> CreateExchangeChannelByType(RabbitExchangeType exchangeType)
        {
            try
            {
                await _connectionLock.WaitAsync();
                if (_connection == null || !_connection.IsOpen)
                    _connection = await _factory.CreateConnectionAsync();

                if (_channel == null || !_channel.IsOpen)
                {
                    string type = exchangeType.ToString().ToLower();
                    _channel = await _connection.CreateChannelAsync();
                    await _channel.ExchangeDeclareAsync(exchange: exchangeName, type: type, autoDelete: true);
                }
                return _channel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erorr orcured when create channel:{ex.ToString()}");
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }
        public async Task SendMessageToExchange(byte[] message, RabbitExchangeType exchangeType, string bindingKey = "")
        {
            if (_channel is null || !_channel.IsOpen)
            {
                await CreateExchangeChannelByType(exchangeType);
            }            
            await _channel.BasicPublishAsync(exchange: exchangeName,
                routingKey: bindingKey,
                body: message);
        }
        public async Task SendMessage(byte[] message)
        {
            if(_channel is null || !_channel.IsOpen)
            {
                await CreateChannel();
            }
            var properties = new BasicProperties
            {
                Persistent = true
            };
            await _channel.BasicPublishAsync(exchange: string.Empty,
                routingKey: queueName,
                mandatory: true,
                basicProperties: properties,
                body: message);
        }
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
