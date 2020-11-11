using System;
using System.Text;
using Insight.Authorization.Jwt.Const;
using Insight.Authorization.Jwt.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Insight.Authorization.Jwt.Extensions
{
	public static class ServiceCollectionEx
	{
		public static IServiceCollection AddJwtAuthorization(this IServiceCollection services,
			JwtTokenOptions jwtTokenOptions,
			JwtServiceOptions jwtServiceOptions = null,
			bool requireHttpsMetadata = false)
		{
			if (jwtTokenOptions == null)
				throw new ArgumentNullException(nameof(jwtTokenOptions));

			jwtServiceOptions ??= new JwtServiceOptions();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = requireHttpsMetadata;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = jwtTokenOptions.Issuer,
						ValidAudience = jwtTokenOptions.Audience,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Key))
					};
				});

			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();
				options.AddPolicy(Policies.ApiAccess,
					policy =>
					{
						policy.AuthenticationSchemes = new[] {JwtBearerDefaults.AuthenticationScheme};
						policy.RequireClaim(jwtServiceOptions.RolClaimName,
							RolValues.ApiAccess);
					});
				options.AddPolicy(Policies.Refresh,
					policy =>
					{
						policy.AuthenticationSchemes = new[] {JwtBearerDefaults.AuthenticationScheme};
						policy.RequireClaim(jwtServiceOptions.RolClaimName,
							RolValues.RefreshAccess);
					});
			});

			return services;
		}

		public static IServiceCollection AddJwtAuthorization(this IServiceCollection services,
			IConfiguration configuration, bool requireHttpsMetadata = false)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			var jwtTokenOptions = configuration.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();
			var jwtServiceOptions = configuration.GetSection(nameof(JwtServiceOptions)).Get<JwtServiceOptions>();

			return services.AddJwtAuthorization(jwtTokenOptions, jwtServiceOptions, requireHttpsMetadata);
		}

		public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			services.Configure<JwtTokenOptions>(configuration.GetSection(nameof(JwtTokenOptions)));
			services.Configure<JwtServiceOptions>(configuration.GetSection(nameof(JwtServiceOptions)));

			services.AddScoped<IJwtService, JwtService>();

			return services;
		}
	}
}