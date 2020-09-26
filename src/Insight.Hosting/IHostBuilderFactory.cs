using System;
using Microsoft.Extensions.Hosting;

namespace Insight.Hosting
{
	public interface IHostBuilderFactory
	{
		IHostBuilder CreateHostBuilder(string[] args, string baseDirectory = null);
	}
}