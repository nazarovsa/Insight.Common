using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Insight.Channels.Tests
{
	public sealed class EnumerableExtensionsTest
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public EnumerableExtensionsTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public async Task Should_sink_many()
		{
			var enumerable = Enumerable.Range(0, 1_000);

			long sum = 0;

			var ints = enumerable as int[] ?? enumerable.ToArray();
			var sw = new Stopwatch();
			sw.Start();
			await ints
				.AsChannelReader()
				.Split(100)
				.SinkMany(async (number) =>
				{
					Interlocked.Add(ref sum, number);
					await Task.Delay(1);
				});


			Assert.Equal(ints.Sum(x => x), sum);
			_testOutputHelper.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
		}
	}
}