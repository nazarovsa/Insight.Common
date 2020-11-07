using System;

namespace Insight.Logging
{
	public interface ILogService
	{
		void Info(object obj, string message, params object[] parameters);

		void Warn(object obj, string message, params object[] parameters);

		void Error(object obj, string message, params object[] parameters);

		void Error(object obj, Exception exception, string message, params object[] parameters);

		void Trace(object obj, string message, params object[] parameters);
	}
}