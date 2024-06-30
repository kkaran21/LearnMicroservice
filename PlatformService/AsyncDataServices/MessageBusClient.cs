using System.Text.Json;
using System.Text.Json.Serialization;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IModel _channel;
        private readonly IConnection _conn;


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
                Console.WriteLine(" connected to rabbit mq");
                _conn.ConnectionShutdown += RabbitMq_ConnectionShutDown;



            }
            catch (System.Exception e)
            {
                Console.WriteLine($"could not connect to rabbit mq {e.Message}");
            }
        }

        private void RabbitMq_ConnectionShutDown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("---> rabbitmq connection shutdown");

        }

        public void PublishToPlatform(PlatformPublishDto platformPublishDto)
        {
            if (_conn.IsOpen)
            {
                publishMessage(platformPublishDto);
            }
            else
            {
                Console.WriteLine("Connection closed message will not be sent");
            }
        }

        private void publishMessage(dynamic Message)
        {

            var message = JsonSerializer.Serialize(Message);
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, messageBodyBytes);
            Console.WriteLine("we have published message");
        }

        public void dispose()
        {
            Console.WriteLine("MessageBus disposed");
            if (_conn.IsOpen)
            {
                _channel.Close();
                _conn.Close();
            }
        }
    }
}