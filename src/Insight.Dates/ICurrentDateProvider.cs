using System;

namespace Insight.Dates
{
	public interface ICurrentDateProvider
	{
		DateTimeOffset UtcDateTimeOffset { get; }

		DateTimeOffset LocalDateTimeOffset { get; }

		DateTime UtcDateTime { get; }

		DateTime LocalDateTime { get; }
	}
}