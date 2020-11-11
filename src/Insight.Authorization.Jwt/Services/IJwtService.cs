using System.Security.Claims;

namespace Insight.Authorization.Jwt
{
	public interface IJwtService
	{
		public string GetAccessToken(params Claim[] claims);

		public string GetRefreshToken(params Claim[] claims);
	}
}