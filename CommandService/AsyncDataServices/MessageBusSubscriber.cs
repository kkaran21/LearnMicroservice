using System.Text;
using System.Text.Json;
using CommandService.EventProcessor;
using RabbitMQ.Client;

namespace CommandService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IConfiguration _config;
        private IModel _channel;
        private IConnection _conn;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
            _config = configuration;
            initializeRabbitMQ();
        }

        public void initializeRabbitMQ()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Port = int.Parse(_config["RabbitMqPort"]);
            factory.HostName = _config["RabbitMqHost"];

            try
            {
                _conn = factory.CreateConnection();
                _channel = _conn.CreateModel();
                _channel.ExchangeDeclare(exchange:"trigger",  type:ExchangeType.Fanout);
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(_queueName, exchange: "trigger", routingKey: "", null);
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

        public void dispose()
        {
            Console.WriteLine("MessageBus disposed");
            if (_conn.IsOpen)
            {
                _channel.Close();
                _conn.Close();
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
                            {
                                var body = ea.Body.ToArray();
                                // copy or deserialise the payload
                                // and process the message
                                // ...
                                var notificationMsg = Encoding.UTF8.GetString(body);
                                _eventProcessor.processEvent(notificationMsg); //processing each event
                                Console.WriteLine("processing event");
                                _channel.BasicAck(ea.DeliveryTag, false);
                            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }
    }
}