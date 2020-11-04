using System;

namespace Insight.DataAccess
{
	/// <summary>
	/// ConnectionOptions
	/// </summary>
	public sealed class ConnectionOptions
	{
		private string _connectionString;

		/// <summary>
		/// Connnection options
		/// </summary>
		public ConnectionOptions()
		{
		}

		/// <summary>
		/// Connnection options
		/// </summary>
		/// <param name="connectionString">Connection string</param>
		/// <exception cref="ArgumentException"><paramref name="connectionString"/>Connection string is empty</exception>
		public ConnectionOptions(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentException(nameof(connectionString));

			_connectionString = connectionString;
		}

		/// <summary>
		/// Conenction string
		/// </summary>
		/// <exception cref="ArgumentException">Connection string is empty</exception>
		public string ConnectionString
		{
			get { return _connectionString; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException(nameof(value));

				_connectionString = value;
			}
		}
	}
}