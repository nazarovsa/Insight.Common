using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Insight.Authorization.Jwt.Const;
using Insight.Authorization.Jwt.Options;
using Insight.Dates;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Insight.Authorization.Jwt
{
	public sealed class JwtService : IJwtService
	{
		private readonly ICurrentDateProvider _dateProvider;
		private readonly JwtServiceOptions _jwtServiceOptions;
		private readonly JwtTokenOptions _jwtTokenOptions;

		public JwtService(IOptionsSnapshot<JwtServiceOptions> jwtServiceOptions,
			IOptionsSnapshot<JwtTokenOptions> jwtTokenOptions,
			ICurrentDateProvider dateProvider)
		{
			_dateProvider = dateProvider ?? throw new ArgumentNullException(nameof(dateProvider));
			_jwtServiceOptions = jwtServiceOptions?.Value ?? throw new ArgumentNullException(nameof(jwtServiceOptions));
			_jwtTokenOptions = jwtTokenOptions?.Value ?? throw new ArgumentNullException(nameof(jwtTokenOptions));
		}

		public string GetAccessToken(params Claim[] claims) => GetToken(
			GetClaimsIdentity(claims, RolValues.ApiAccess),
			_jwtTokenOptions.AccessTokenLifetime);

		public string GetRefreshToken(params Claim[] claims) => GetToken(
			GetClaimsIdentity(claims, RolValues.RefreshAccess),
			_jwtTokenOptions.RefreshTokenLifetime);

		private string GetToken(ClaimsIdentity identity, TimeSpan lifetime)
		{
			var now = _jwtServiceOptions.DateTimeHandling == DateTimeHandling.Local
				? _dateProvider.LocalDateTime
				: _dateProvider.UtcDateTime;
			
			var jwt = new JwtSecurityToken(
				_jwtTokenOptions.Issuer,
				_jwtTokenOptions.Audience,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(lifetime),
				signingCredentials: new SigningCredentials(_jwtTokenOptions.GetSymmetricSecurityKey(),
					_jwtTokenOptions.Algorithm));

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		private ClaimsIdentity GetClaimsIdentity(IReadOnlyCollection<Claim> claims, string rolClaimValue)
		{
			if (claims == null)
				throw new ArgumentNullException(nameof(claims));

			var claimsList = claims.ToList();
			claimsList.Add(new Claim(_jwtServiceOptions.RolClaimName, rolClaimValue));

			var identity = new ClaimsIdentity(claimsList, "Token", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

			return identity;
		}
	}
}