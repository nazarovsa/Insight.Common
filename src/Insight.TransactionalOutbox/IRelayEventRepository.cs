using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Insight.TransactionalOutbox
{
	public interface IRelayEventRepository<TEvent>
		where TEvent : RelayEventBase
	{
		Task<TEvent> Add(TEvent @event, CancellationToken cancellationToken = default);

		Task<TEvent> GetUnprocessedEvent(CancellationToken cancellationToken = default);

		Task<IReadOnlyCollection<TEvent>> GetUnprocessedEvents(CancellationToken cancellationToken = default);
		
		Task<IReadOnlyCollection<TEvent>> GetUnprocessedEvents(int count, CancellationToken cancellationToken = default);

		Task Delete(Guid id, CancellationToken cancellationToken = default);
	}
}