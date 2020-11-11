using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Insight.Authorization.Jwt.Options
{
	public sealed class JwtTokenOptions
	{
		/// <summary>
		/// Token issuer
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// Token comsumer
		/// </summary>
		public string Audience { get; set; }

		/// <summary>
		/// Secret key
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Security algorithm
		/// </summary>
		public string Algorithm { get; set; }

		/// <summary>
		/// Lifetime of access token
		/// </summary>
		public TimeSpan AccessTokenLifetime { get; set; }

		/// <summary>
		/// Lifetime of refresh token
		/// </summary>
		public TimeSpan RefreshTokenLifetime { get; set; }

		public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
	}
}