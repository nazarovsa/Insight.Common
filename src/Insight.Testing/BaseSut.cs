namespace Insight.Testing
{
	public abstract class BaseSut
	{
		public ServiceBag ServiceBag { get; private set; }

		protected BaseSut()
		{
			ServiceBag = new ServiceBag();
		}
	}
}