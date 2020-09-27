using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Xunit;

namespace Insight.Reactive.RabbitMQ.Tests
{
    public sealed class RabbitMqConsumerTest
    {
        private readonly RabbitMqConsumer _consumer;

        private static ConsumerOptions Options => new ConsumerOptions
        {
            Host = "localhost",
            Queue = "test-queue",
            QueueOptions = new QueueOptions
            {
                DeclareQueue = true
            },
            AutoAck = false,
        };

        public RabbitMqConsumerTest()
        {
            _consumer = new RabbitMqConsumer(Options);
        }

        [Fact(Skip = "RabbitMq on host required")]
        public async Task Should_receive_five_messages()
        {
            var result = new ConcurrentBag<string>();
            using (_consumer
                .AsObservable()
                .Do(x =>
                {
                    var body = x.Body.ToArray();
                    var message = Encoding.UTF8.GetString((byte[]) body);
                    result.Add(message);
                    _consumer.Ack(x.DeliveryTag);
                })
                .Subscribe())
            {
                for (int i = 0; i < 5; i++)
                {
                    SendMessage($"Message {i}");
                }

                await Task.Delay(1000);

                Assert.Equal(5, result.Count);
            }
        }

        private void SendMessage(string text)
        {
            var factory = new ConnectionFactory {HostName = Options.Host};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(text);

            channel.BasicPublish("",
                Options.Queue,
                null,
                body);
        }
    }
}