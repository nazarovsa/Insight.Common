namespace Insight.Authorization.Jwt.Options
{
	public sealed class JwtServiceOptions
	{
		public string RolClaimName { get; set; } = "rol";

		public DateTimeHandling DateTimeHandling { get; set; } = DateTimeHandling.Utc;
	}

	public enum DateTimeHandling
	{
		Undefined = 0,
		Local,
		Utc
	}
}