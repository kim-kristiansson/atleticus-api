using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AtleticusAPI.Extensions
{
    public static class JwtBearerAuthenticationExtension
    {
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey)) {
                throw new InvalidOperationException("JWT Secret Key is not configured.");
            }

            var issuer = configuration["JwtSettings:Issuer"];
            if (string.IsNullOrEmpty(issuer)) {
                throw new InvalidOperationException("JWT Issuer is not configured.");
            }

            var validAudiences = configuration.GetSection("JwtSettings:ValidAudiences").Get<string[]>();
            if (validAudiences == null || validAudiences.Length == 0) {
                throw new InvalidOperationException("JWT Valid Audiences are not configured.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudiences = validAudiences,
                    };
                });

            return services;
        }
    }
}
