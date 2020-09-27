using System;
using System.Reactive.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Insight.Reactive.RabbitMQ
{
    public sealed class RabbitMqConsumer : IRabbitMqConsumer, IDisposable
    {
        private readonly ConsumerOptions _options;

        private IConnection _connection;

        private IModel _channel;

        private EventingBasicConsumer _consumer;

        public bool Disposed { get; private set; }

        public RabbitMqConsumer(ConsumerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Host))
                throw new ArgumentException("Host should be specified");

            if (string.IsNullOrWhiteSpace(options.Queue))
                throw new ArgumentException("Queue should be specified");

            _options = options;
        }

        public IObservable<BasicDeliverEventArgs> AsObservable()
        {
            EnsureConnectionCreated();

            return Observable.FromEventPattern<BasicDeliverEventArgs>(x =>
                {
                    if (_options.QueueOptions.DeclareQueue)
                    {
                        _channel.QueueDeclare(_options.Queue,
                            _options.QueueOptions.Durable,
                            _options.QueueOptions.Exclusive,
                            _options.QueueOptions.AutoDelete,
                            _options.QueueOptions.Arguments);
                    }

                    _consumer.Received += x;
                    _channel.BasicConsume(_options.Queue,
                        _options.AutoAck,
                        _consumer);
                }, x =>
                {
                    _consumer.Received -= x;
                    _channel.Close();
                    _channel.Dispose();
                    _connection.Dispose();
                    _channel = null;
                    _connection = null;
                    _consumer = null;
                })
                .Select(x => x.EventArgs)
                .AsObservable();
        }

        public void Ack(ulong deliveryTag, bool multiple = false)
            => _channel?.BasicAck(deliveryTag, multiple);

        private void EnsureConnectionCreated()
        {
            if (_consumer != null)
                return;

            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                RequestedHeartbeat = _options.RequestedHeartbeat
            };

            if (_options.AuthenticationEnabled)
            {
                factory.UserName = _options.Username;
                factory.Password = _options.Password;
            }

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _consumer = new EventingBasicConsumer(_channel);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    _channel.Dispose();
                    _connection.Dispose();
                }

                Disposed = true;
            }
        }

        ~RabbitMqConsumer()
        {
            Dispose(false);
        }
    }
}