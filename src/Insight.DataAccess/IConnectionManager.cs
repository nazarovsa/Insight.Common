using System;
using System.Data;

namespace Insight.DataAccess
{
	public interface IConnectionManager : IDisposable
	{
		IDbConnection GetConnection();

		IDbTransaction GetTransaction();

		IDisposable BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);

		void CommitTransaction();

		void RollbackTransaction();
	}
}