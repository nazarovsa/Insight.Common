using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Insight.Reactive.RabbitMQ.Samples
{
    public sealed class RabbitConsumerWebHost : IHostedService
    {
        private readonly IRabbitMqConsumer _client;

        private IDisposable _subscription;

        public RabbitConsumerWebHost(IRabbitMqConsumer client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscription = _client
                .AsObservable()
                .Do(x =>
                {
                    var body = x.Body.ToArray();
                    var message = Encoding.UTF8.GetString((byte[]) body);
                    Console.WriteLine($" [{DateTime.UtcNow:g}] Received {message}");
                    _client.Ack(x.DeliveryTag);
                }, ex => throw ex)
                .Subscribe();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscription?.Dispose();
            return Task.CompletedTask;
        }
    }
}