using System.Collections.Generic;

namespace Insight.Reactive.RabbitMQ
{
    /// <summary>
    /// Queue options
    /// </summary>
    public sealed class QueueOptions
    {
        /// <summary>
        /// If true, consumer will declare a queue
        /// </summary>
        public bool DeclareQueue { get; set; }

        public bool Durable { get; set; } = true;

        public bool Exclusive { get; set; }

        public bool AutoDelete { get; set; }

        public Dictionary<string, object> Arguments { get; set; } = null;
    }
}