using System.Security.Claims;

namespace Insight.Authorization.Jwt.Models
{
	public sealed class TokenIdentity
	{
		public ClaimsIdentity AccessTokenIdentity { get; set; }

		public ClaimsIdentity RefreshTokenIdentity { get; set; }
	}
}