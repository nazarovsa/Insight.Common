using System;

namespace Insight.Reactive.RabbitMQ
{
    public sealed class ConsumerOptions
    {
        /// <summary>
        /// Hostname
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Queue name
        /// </summary>
        public string Queue { get; set; }

        /// <summary>
        /// Queue options
        /// </summary>
        public QueueOptions QueueOptions { get; set; } = new QueueOptions();

        /// <summary>
        /// If enabled rabbit client will ack messages automatically
        /// </summary>
        public bool AutoAck { get; set; } = false;

        /// <summary>
        /// Heartbeat time
        /// </summary>
        public TimeSpan RequestedHeartbeat { get; set; } = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Username if authorization required
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password if authorization required
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// True when <see cref="Username"/> and <see cref="Password"/> provided
        /// </summary>
        public bool AuthenticationEnabled =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }
}