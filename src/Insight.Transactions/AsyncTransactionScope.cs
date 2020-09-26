using System;
using System.Transactions;

namespace Insight.Transactions
{
	public sealed class AsyncTransactionScope : IDisposable
	{
		private TransactionScope _scope;

		public bool Disposed { get; private set; }

		public AsyncTransactionScope()
		{
			Initialize();
		}

		public AsyncTransactionScope(TransactionScopeOption scopeOption = TransactionScopeOption.Required,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			Initialize(scopeOption, isolationLevel);
		}

		private void Initialize(TransactionScopeOption scopeOption = TransactionScopeOption.Required,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			if (Transaction.Current != null)
				_scope = new TransactionScope(Transaction.Current, TransactionScopeAsyncFlowOption.Enabled);
			else
				_scope = new TransactionScope(scopeOption, new TransactionOptions
				{
					IsolationLevel = isolationLevel,
				}, TransactionScopeAsyncFlowOption.Enabled);
		}

		public void Complete()
		{
			_scope.Complete();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					_scope.Dispose();
				}

				Disposed = true;
			}
		}

		~AsyncTransactionScope()
		{
			Dispose(false);
		}
	}
}