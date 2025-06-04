using ApiClient.RefitInterfaces;
using Application.Entities.Auth;
using Application.Interfaces.Auth;
using Application.Interfaces.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.OpenApi.Models;
using Application.Interfaces.OAuth2;
using Application.Services.OAuth2;

namespace ProjetosApi.Ioc
{
    public static class IndependencyInjection
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto integração", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IFipeService, FipeService>();
            services.AddScoped<IIbgeService, IbgeService>();
            services.AddScoped<IAuthService, AuthService>();
        }
        public static void AddRefitSettings(this IServiceCollection services)
        {
            services.AddRefitClient<ICepRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });
            services.AddRefitClient<IFipeRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });
            services.AddRefitClient<IIbgeRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });
        }
        public static void AddToken(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A2D8u9jWwWnKlQ27@pN7XzL5bQyP9v3l2cT5y0jZf8yM1mZ2tG7uO0c3QhVwD9bA")),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddOAuth2(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

        }
    }
}
