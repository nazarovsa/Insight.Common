using System;

namespace Insight.Hosting.Worker.Options
{
	public sealed class BackgroundServiceOptions
	{
		public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

		public TimeSpan ExceptionDelay { get; set; } = TimeSpan.FromSeconds(5);
	}
}