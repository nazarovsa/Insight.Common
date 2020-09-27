using System;
using RabbitMQ.Client.Events;

namespace Insight.Reactive.RabbitMQ
{
    public interface IRabbitMqConsumer
    {
        IObservable<BasicDeliverEventArgs> AsObservable();

        void Ack(ulong deliveryTag, bool multiple = false);
    }
}