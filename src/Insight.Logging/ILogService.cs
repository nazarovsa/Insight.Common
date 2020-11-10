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

	public interface ILogService<T>
	{
		void Info(string message, params object[] parameters);

		void Warn(string message, params object[] parameters);

		void Error(string message, params object[] parameters);

		void Error(Exception exception, string message, params object[] parameters);

		void Trace(string message, params object[] parameters);
	}
}