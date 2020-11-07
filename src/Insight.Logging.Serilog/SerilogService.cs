using System;
using Serilog;

namespace Insight.Logging.Serilog
{
	public class SerilogService : ILogService
	{
		public void Info(object obj, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(obj))
				.Information(message, parameters);
		}

		public void Warn(object obj, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(obj))
				.Warning(message, parameters);
		}

		public void Error(object obj, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(obj))
				.Error(message, parameters);
		}

		public void Error(object obj, Exception exception, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(obj))
				.Error(exception, message, parameters);
		}

		public void Trace(object obj, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(obj))
				.Verbose(message, parameters);
		}

		private string GetContext(object obj)
		{
			if (obj is Type type)
				return type.GetContext(true);

			return obj.GetType().GetContext(true);
		}
	}
}