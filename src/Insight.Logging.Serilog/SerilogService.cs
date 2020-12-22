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

	public sealed class SerilogService<T> : SerilogService, ILogService<T>
	{
		public void Info(string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(typeof(T)))
				.Information(message, parameters);
		}

		public void Warn(string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(typeof(T)))
				.Warning(message, parameters);
		}

		public void Error(string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(typeof(T)))
				.Error(message, parameters);
		}

		public void Error(Exception exception, string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(typeof(T)))
				.Error(exception, message, parameters);
		}

		public void Trace(string message, params object[] parameters)
		{
			Log.ForContext("SourceContext", GetContext(typeof(T)))
				.Verbose(message, parameters);
			;
		}

		private string GetContext(object obj)
		{
			if (obj is Type type)
				return type.GetContext(true);

			return obj.GetType().GetContext(true);
		}
	}
}