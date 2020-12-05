using System;

namespace Insight.TransactionalOutbox
{
	public abstract class RelayEventBase
	{
		public Guid Id { get; set; }
	}
}