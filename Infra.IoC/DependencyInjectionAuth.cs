using System.Text;
using Infra.Identity.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infra.IoC;

public static class DependencyInjectionAuth
{
    public static IServiceCollection AddInfrastructureAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtAppSettingOptions = configuration.GetSection("Authentication:JwtOptions");

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration.GetSection("Authentication:JwtOptions:SecurityKey").Value));

        var googleAuthOptions = configuration.GetSection("Authentication:Google");

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtAppSettingOptions[nameof(JwtOptions.Issuer)];
            options.Audience = jwtAppSettingOptions[nameof(JwtOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            options.AccessTokenExpiration =
                int.Parse(jwtAppSettingOptions[nameof(JwtOptions.AccessTokenExpiration)] ?? "0");
            options.RefreshTokenExpiration =
                int.Parse(jwtAppSettingOptions[nameof(JwtOptions.RefreshTokenExpiration)] ?? "0");
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("Authentication:JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("Authentication:JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.Audience = configuration.GetSection("Authentication:JwtOptions:Audience").Value;

                opt.TokenValidationParameters = tokenValidationParameters;
                opt.SaveToken = true;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(googleOptions =>
            {
                googleOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                googleOptions.ClientId = googleAuthOptions[nameof(GoogleOptions.ClientId)];
                googleOptions.ClientSecret = googleAuthOptions[nameof(GoogleOptions.ClientSecret)];
            });

        // services.AddAuthorization(options =>
        // {
        //     options.FallbackPolicy = new AuthorizationPolicyBuilder()
        //         .RequireAuthenticatedUser()
        //         .Build();
        // });

        // services.AddHttpContextAccessor();


        return services;
    }
}