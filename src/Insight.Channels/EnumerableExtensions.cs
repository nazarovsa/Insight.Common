using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Insight.Channels
{
	public static class EnumerableExtensions
	{
		public static ChannelReader<T> AsChannelReader<T>(this IEnumerable<T> source)
		{
			var channel = Channel.CreateUnbounded<T>();

			Task.Run(async () =>
			{
				foreach (var item in source)
				{
					await channel.Writer.WriteAsync(item);
				}

				channel.Writer.Complete();
			});

			return channel.Reader;
		}

		public static IEnumerable<ChannelReader<T>> Split<T>(this ChannelReader<T> reader, int n)
		{
			var outputs = new Channel<T>[n];
			for (var i = 0; i < n; i++) outputs[i] = Channel.CreateUnbounded<T>();

			var index = 0;
			Task.Run(async () =>
			{
				await foreach (var item in reader.ReadAllAsync())
				{
					await outputs[index].Writer.WriteAsync(item);
					index = (index + 1) % n;
				}

				foreach (var channel in outputs)
				{
					channel.Writer.Complete();
				}
			});

			return outputs.Select(ch => ch.Reader);
		}

		public static async Task Sink<T>(this ChannelReader<T> reader, Func<T, Task> action,
			Func<T, Exception, Task> onError = null, CancellationToken cancellationToken = default)
		{
			await foreach (var value in reader.ReadAllAsync(cancellationToken))
			{
				try
				{
					await action(value);
				}
				catch (Exception ex)
				{
					if (onError != null)
						await onError(value, ex);
					else
						throw;
				}
			}
		}

		public static Task SinkMany<T>(this IEnumerable<ChannelReader<T>> readers, Func<T, Task> action,
			Func<T, Exception, Task> onError = null, CancellationToken cancellationToken = default)
		{
			return Task.WhenAll(readers.Select(x => x.Sink(action, onError, cancellationToken)));
		}
	}
}