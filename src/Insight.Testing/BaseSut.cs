namespace Insight.Testing
{
	public abstract class BaseSut
	{
		protected ServiceBag ServiceBag { get; private set; }

		protected BaseSut()
		{
			ServiceBag = new ServiceBag();
		}
	}
}