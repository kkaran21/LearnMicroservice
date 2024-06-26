using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IModel  _channel;
            private readonly IConnection  _conn;


        public MessageBusClient(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionFactory factory = new ConnectionFactory();
            factory.Port = int.Parse(_config["RabbitMqPort"]);
            factory.HostName = _config["RabbitMqHost"];

            try
            {
                 _conn = factory.CreateConnection();
                _channel = _conn.CreateModel();
                _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
                // _conn.ConnectionShutdown +=
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void PublishToPlatform(PlatformPublishDto platformPublishDto)
        {
            throw new NotImplementedException();
        }
    }
}