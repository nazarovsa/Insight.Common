using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Insight.TransactionalOutbox
{
	public interface IRelayEventProcessor<in TEvent>
		where TEvent : RelayEventBase
	{
		Task ProcessEvents(IReadOnlyCollection<TEvent> events, CancellationToken cancellationToken = default);

		Task ProcessOne(CancellationToken cancellationToken = default);

		Task ProcessMany(int count = 0, CancellationToken cancellationToken = default);
	}
}