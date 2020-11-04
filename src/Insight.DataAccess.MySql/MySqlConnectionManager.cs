using System.Data;
using MySqlConnector;

namespace Insight.DataAccess.MySql
{
	public sealed class MySqlConnectionManager : ConnectionManager
	{
		public MySqlConnectionManager(ConnectionOptions connectionOptions) : base(connectionOptions)
		{
		}

		protected override IDbConnection CreateConnection() => new MySqlConnection(ConnectionOptions.ConnectionString);
	}
}